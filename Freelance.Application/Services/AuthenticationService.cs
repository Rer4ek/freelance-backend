using Freelance.Core.Abstraction;

namespace Freelance.Application.Services
{
    public class AuthenticationService
    {

        private IUserRepository _userRepository;
        private ISessionRepository _sessionRepository;

        public AuthenticationService(IUserRepository userRepository, ISessionRepository sessionRepository)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }


    }
}
