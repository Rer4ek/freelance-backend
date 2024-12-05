using CSharpFunctionalExtensions;
using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface IUserService
    {
        Task<Result<Guid>> CreateUser(User user);
        Task<Result<Guid>> UpdateUser(User user);
        Task<Result<User>> GetUserBySession(string sessionHash);
        Task<Result<User>> GetUserByLogin(string login);
    }
}