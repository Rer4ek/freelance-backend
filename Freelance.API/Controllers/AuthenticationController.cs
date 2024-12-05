using CSharpFunctionalExtensions;
using Freelance.API.Contracts;
using Freelance.Core.Abstraction;
using Freelance.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController: ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Authentication(AuthenticationRequest authenticationRequest)
        {

            Result<AuthenticationData> authenticationDataResult = AuthenticationData.Create(
                authenticationRequest.Login,
                authenticationRequest.Password
            );

            if (authenticationDataResult.IsFailure)
            {
                return BadRequest(authenticationDataResult.Error);
            }


            Result<string> hashResult = await _authenticationService.Authentication(authenticationDataResult.Value);

            if (hashResult.IsFailure)
            {
                return BadRequest(hashResult.Error);
            }

            return Ok(hashResult.Value);
        }

        [HttpPut("{token}")]
        public async Task<ActionResult> Delete(string token)
        {
            Result result = await _authenticationService.DeleteActiveSession(token);
            return Ok();
        }
    }
}
