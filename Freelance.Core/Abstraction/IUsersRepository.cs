using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface IUsersRepository
    {
        Task<Guid> Create(User user);
        Task<List<User>> Get();
        Task<Guid> Update(Guid guid, string name, string discription, string resume, string password);
    }
}