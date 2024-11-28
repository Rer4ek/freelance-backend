using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface IUsersRepository
    {
        Task<List<User>> Get();
    }
}