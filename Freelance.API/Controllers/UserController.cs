using CSharpFunctionalExtensions;
using Freelance.API.Contracts;
using Freelance.Core.Abstraction;
using Freelance.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService usersService)
        {
            _userService = usersService;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] UserRequest request)
        {
            Guid userId = await _userService.UpdateUser(id, request.Name, request.Description, request.Resume,
                request.Photo, request.Password, request.Login);

            return Ok(userId);
        }
    }
}
