using CSharpFunctionalExtensions;

namespace Freelance.Core.Models
{
    public class User
    {
        public const int MAX_NAME_LENGHT = 50;
        public const int MAX_DISCRIPTION_LENGHT = 500;
        public const int MAX_PASSWORD_LENGHT = 50;
        public const int MIN_PASSWORD_LENGHT = 6;

        private User(Guid guid, string name, string? discription, string? resume, string password)
        {
            Id = guid;
            Name = name;
            Discription = discription;
            Resume = resume;
            Password = password;
        }

        public static Result<User> Create(Guid guid, string name, string? discription, string? resume, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGHT)
            {
                return Result.Failure<User>($"Имя не должно быть пустым и длина должна быть менее {MAX_NAME_LENGHT} символов");
            }

            if (discription != null && discription.Length > MAX_DISCRIPTION_LENGHT)
            {
                return Result.Failure<User>($"Описание не должно быть более {MAX_DISCRIPTION_LENGHT} символов");
            }

            if (string.IsNullOrEmpty(password))
            {
                return Result.Failure<User>($"Пароль не должен быть пустым");
            }

            if (password.Length > MAX_PASSWORD_LENGHT)
            {
                return Result.Failure<User>($"Пароль не должен быть более {MAX_PASSWORD_LENGHT} символов");
            }

            if (password.Length < MIN_PASSWORD_LENGHT)
            {
                return Result.Failure<User>($"Пароль должен быть не менее {MIN_PASSWORD_LENGHT} символов");
            }

            return Result.Success<User>(new User(guid, name, discription, resume, password));
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string? Discription { get; private set; } = string.Empty;

        public string Password { get; private set; } = string.Empty;

        public string? Resume { get; private set; } // Resume type
    }
}
