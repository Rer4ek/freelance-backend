using Freelance.Core.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController: ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPut("{token}")]
        public async Task<ActionResult> CheckValid(string token)
        {
            bool result = await _authorizationService.IsValidSession(token);
            return Ok(result);
        }

    }
}
