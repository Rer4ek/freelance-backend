using CSharpFunctionalExtensions;
using Freelance.Core.Abstraction;
using Freelance.Core.Models;
using Freelance.Core.Utils;

namespace Freelance.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IResumeRepository _resumeRepository;

        public UserService(IUserRepository userRepository, IPhotoRepository photoRepository, IResumeRepository resumeRepository)
        {
            _userRepository = userRepository;
            _photoRepository = photoRepository;
            _resumeRepository = resumeRepository;
        }

        public async Task<Result<Guid>> CreateUser(User user)
        {
            User? userByLogin = await _userRepository.GetByLogin(user.Login);

            if (userByLogin != null)
            {
                return Result.Failure<Guid>("Пользователь уже существует");
            }

            Result resultPassword = ValidatePassword(user.Password);

            if (resultPassword.IsFailure)
            {
                return Result.Failure<Guid>(resultPassword.Error);
            }

            await _photoRepository.Create(user.Photo);
            await _resumeRepository.Create(user.Resume);
            Guid id = await _userRepository.Create(user);
            return Result.Success(id);

        }

        public async Task<Result<Guid>> UpdateUser(User user)
        {
            Result resultPassword = ValidatePassword(user.Password);

            if (resultPassword.IsFailure)
            {
                return Result.Failure<Guid>(resultPassword.Error);
            }

            await _photoRepository.Update(user.Photo);
            await _resumeRepository.Update(user.Resume);
            return await _userRepository.Update(user);
        }

        private Result ValidatePassword(string password)
        {
            if (password.Length < User.MIN_PASSWORD_LENGHT)
            {
                return Result.Failure($"Пароль меньше {User.MIN_PASSWORD_LENGHT}");
            }

            if (password.Length > User.MAX_PASSWORD_LENGHT)
            {
                return Result.Failure($"Пароль больше {User.MAX_PASSWORD_LENGHT}");
            }

            return Result.Success();
        }

        public async Task<Result<User>> GetUserBySession(string sessionHash)
        {
            User? user = await _userRepository.GetBySession(sessionHash);
            if (user == null)
            {
                return Result.Failure<User>("Пользователь нету");
            }

            return Result.Success<User>(user);
        }

        public async Task<Result<User>> GetUserByLogin(string login)
        {
            User? user = await _userRepository.GetByLogin(login);
            if (user == null)
            {
                return Result.Failure<User>("Пользователь нету");
            }

            return Result.Success<User>(user);
        }
    }
}
