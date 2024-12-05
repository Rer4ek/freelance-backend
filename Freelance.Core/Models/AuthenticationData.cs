using CSharpFunctionalExtensions;

namespace Freelance.Core.Models
{
    public class AuthenticationData
    {
        private AuthenticationData(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;

        public static Result<AuthenticationData> Create(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return Result.Failure<AuthenticationData>("Логин пустой");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Failure<AuthenticationData>("Пароль пустой");
            }

            return Result.Success<AuthenticationData>(new AuthenticationData(login, password));
        } 
    }
}
