using CSharpFunctionalExtensions;
using Freelance.API.Contracts;
using Freelance.Core.Abstraction;
using Freelance.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegistrationController(IUserService usersService)
        {
            _userService = usersService;
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UserRequest request)
        {   
            Result<User> userResult = Core.Models.User.Create(Guid.NewGuid(), request.Name, request.Description,
                request.Resume, request.Photo, request.Password, request.Login);

            await Console.Out.WriteLineAsync(request.Name);

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

    }
}
