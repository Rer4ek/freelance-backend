namespace Freelance.Core.Abstraction
{
    public interface IAuthorizationService
    {
        Task<bool> IsValidSession(string token);
    }
}