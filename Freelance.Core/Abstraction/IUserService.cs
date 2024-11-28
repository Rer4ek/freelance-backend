using CSharpFunctionalExtensions;
using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface IUserService
    {
        Task<Result<Guid>> CreateUser(User user);
        Task<Guid> UpdateUser(Guid guid, string? name, string? description, string? resume, string? photo, string password, string login);
    }
}