using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using Freelance.Core.Abstraction;
using Freelance.Core.Models;
using Freelance.Core.Utils;

namespace Freelance.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private IUserRepository _userRepository;
        private ISessionRepository _sessionRepository;

        public AuthenticationService(IUserRepository userRepository, ISessionRepository sessionRepository)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<Result<string>> Authentication(AuthenticationData authenticationData)
        {
            User? user = await _userRepository.GetByLogin(authenticationData.Login);

            if (user == null || !Encryption.Compare(user.Password, authenticationData.Password))
            {
                return Result.Failure<string>("Неверные данные для входа");
            }

            Guid id = await _sessionRepository.Create(user);

            string hash = Encryption.Encrypt(id.ToString());

            return Result.Success<string>(hash);
        }

        public async Task<Result> DeleteActiveSession(string token)
        {
            return await _sessionRepository.Delete(token);
        }

    }
}
