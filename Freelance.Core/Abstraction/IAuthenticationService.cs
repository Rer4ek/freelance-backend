using CSharpFunctionalExtensions;
using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface IAuthenticationService
    {
        Task<Result<string>> Authentication(AuthenticationData authenticationData);

        Task<Result> DeleteActiveSession(string token);
    }
}