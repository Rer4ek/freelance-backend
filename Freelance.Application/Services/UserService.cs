using CSharpFunctionalExtensions;
using Freelance.Core.Abstraction;
using Freelance.Core.Models;

namespace Freelance.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository usersRepository)
        {
            _userRepository = usersRepository;
        }

        public async Task<Result<Guid>> CreateUser(User user)
        {
            User? userByLogin = await _userRepository.GetByLogin(user.Login);

            if (userByLogin != null)
            {
                return Result.Failure<Guid>("Пользователь уже существует");
            }

            if (user.Password.Length < User.MIN_PASSWORD_LENGHT)
            {
                return Result.Failure<Guid>($"Пароль меньше {User.MIN_PASSWORD_LENGHT}");
            }

            if (user.Password.Length > User.MAX_PASSWORD_LENGHT)
            {
                return Result.Failure<Guid>($"Пароль больше {User.MAX_PASSWORD_LENGHT}");
            }

            Guid id = await _userRepository.Create(user);
            return Result.Success(id);

        }

        public async Task<Guid> UpdateUser(Guid guid, string? name, string? description, string? resume,
            string? photo, string password, string login)
        {
            return await _userRepository.Update(guid ,name, description, resume, photo, login, password);
        }
    }
}
