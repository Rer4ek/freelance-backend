using CSharpFunctionalExtensions;
using Freelance.API.Contracts;
using Freelance.Core.Abstraction;
using Freelance.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }


        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            List<User> users = await _usersService.GetUsers();

            List<UserResponse> response = users
                .Select(u => new UserResponse(u.Id, u.Name, u.Discription, u.Password, u.Resume))
                .ToList();

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UserRequest request)
        {
            Result<User> result = Core.Models.User.Create(Guid.NewGuid(), request.Name, request.Discription, request.Resume, request.Password);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            Guid id = await _usersService.CreateUsers(result.Value);

            return Ok(id); 
        }
    }
}
