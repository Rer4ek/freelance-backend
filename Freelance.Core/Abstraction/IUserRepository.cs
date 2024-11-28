using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface IUserRepository
    {
        Task<Guid> Create(User user);

        Task<Guid> Update(Guid guid, string? name, string? description, string? resume, string? photo, string password, string login);

        Task<User?> GetByLogin(string login);
    }
}