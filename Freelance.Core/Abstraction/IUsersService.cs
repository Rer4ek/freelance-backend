using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface IUsersService
    {
        Task<Guid> CreateUsers(User user);
        Task<List<User>> GetUsers();
        Task<Guid> UpdateUsers(Guid guid, string name, string discription, string resume, string password);
    }
}