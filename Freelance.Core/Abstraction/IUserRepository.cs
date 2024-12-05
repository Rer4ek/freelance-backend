using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface IUserRepository
    {
        Task<Guid> Create(User user);
        Task<Guid> Update(User user);
        Task<User?> GetByLogin(string login);
        Task<User?> GetBySession(string sessionHash);
    }
}