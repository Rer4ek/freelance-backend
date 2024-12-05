using CSharpFunctionalExtensions;

namespace Freelance.Core.Models
{
    public class User
    {
        public const int MAX_NAME_LENGHT = 50;
        public const int MAX_Description_LENGHT = 500;
        public const int MAX_PASSWORD_LENGHT = 50;
        public const int MIN_PASSWORD_LENGHT = 6;
        public Guid Id { get; private set; }

        public string? Name { get; private set; } = string.Empty;

        public string? Description { get; private set; } = string.Empty;

        public ClientFile? Resume { get; private set; }

        public ClientFile? Photo {  get; private set; }

        public string Login { get; private set; }

        public string Password { get; private set; }

        private User(Guid guid, string? name, string? description, ClientFile? resume, ClientFile? photo, string password, string login)
        {
            Id = guid;
            Name = name;
            Description = description;
            Resume = resume;
            Photo = photo;
            Password = password;
            Login = login;
        }

        public static Result<User> Create(Guid guid, string? name, string? description,
            ClientFile? resume, ClientFile? photo, string password, string login)
        {
            if (name?.Replace(" ", "") == string.Empty || name?.Length > MAX_NAME_LENGHT)
            {
                return Result.Failure<User>($"Имя не должно быть пустым и длина должна быть менее {MAX_NAME_LENGHT} символов");
            }

            if (description != null &&  description.Length > MAX_Description_LENGHT)
            {
                return Result.Failure<User>($"Описание не должно быть более {MAX_Description_LENGHT} символов");
            }

            if (string.IsNullOrEmpty(password))
            {
                return Result.Failure<User>($"Пароль не должен быть пустым");
            }

            if (login.Length > MAX_NAME_LENGHT)
            {
                return Result.Failure<User>($"Логин не должно быть пустым и длина должна быть менее {MAX_NAME_LENGHT} символов");
            }

            return Result.Success<User>(new User(guid, name, description, resume, photo, password, login));
        }

    }
}
