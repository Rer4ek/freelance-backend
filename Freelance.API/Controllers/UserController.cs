using CSharpFunctionalExtensions;
using Freelance.API.Contracts;
using Freelance.Core.Abstraction;
using Freelance.Core.Models;
using Freelance.Core.Utils;
using Freelance.Core.Сonstants;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;

        public UserController(IUserService userService, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _userService = userService;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromForm] UserRequest request)
        {

            Result<User> userResult = await _userService.GetUserByLogin(request.Login);

            if (userResult.IsFailure)
            {
                return BadRequest(userResult.Error);
            }

            User user = userResult.Value;

            ClientFile? photo = await ClientFileTool
                .Download(FileConstants.PhotoDirectory, request.Photo, user.Photo?.Id);
            if (photo == null)
            {
                photo = user.Photo;
            }
            
            ClientFile? resume = await ClientFileTool
                .Download(FileConstants.ResumeDirectory, request.Resume, user.Resume?.Id);
            if (resume == null)
            {
                resume = user.Resume;
            }

            userResult = Core.Models.User
                .Create(id, request.Name, request.Description, resume, photo, request.Password, request.Login);

            if (userResult.IsFailure)
            {
                return BadRequest(userResult.Error);
            }

            Result<Guid> userIdResult = await _userService.UpdateUser(userResult.Value);

            if (userIdResult.IsFailure)
            {
                return BadRequest(userIdResult.Error);
            }

            return Ok(userIdResult.Value);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UserRequest request)
        {
            Result<User> userResult = Core.Models.User
                .Create(Guid.NewGuid(), request.Name, request.Description, null, null, request.Password, request.Login);

            if (userResult.IsFailure)
            {
                return BadRequest(userResult.Error);
            }

            Result<Guid> IdResult = await _userService.CreateUser(userResult.Value);

            if (IdResult.IsFailure)
            {
                return BadRequest(IdResult.Error);
            }

            return Ok(IdResult.Value);
        }

        [HttpPut("Session/{token}")]
        public async Task<ActionResult<UserResponse>> GetUserBySession(string token)
        {
            bool isValid = await _authorizationService.IsValidSession(token);
            if (!isValid)
            {
                return BadRequest();
            }

            Result<User> userResult = await _userService.GetUserBySession(token);
            if (userResult.IsFailure)
            {
                return BadRequest();
            }

            User user = userResult.Value;

            FileContentResult? photo = await ClientFileTool.Upload(user.Photo?.Path, "/photo.png", FileConstants.PhotoDefaultPath);

            FileContentResult? resume = await ClientFileTool.Upload(user.Resume?.Path, "/resume.dox");


            return Ok(new UserResponse(user.Id, user.Name, user.Description, resume, photo, user.Login));
        }
    }
}
