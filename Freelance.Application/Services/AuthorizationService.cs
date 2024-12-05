using Freelance.Core.Abstraction;

namespace Freelance.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {

        private readonly ISessionRepository _sessionRepository;

        public AuthorizationService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<bool> IsValidSession(string token)
        {
            return await _sessionRepository.IsValid(token);
        }
    }
}
