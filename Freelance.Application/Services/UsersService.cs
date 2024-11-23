using Freelance.Core.Abstraction;
using Freelance.Core.Models;

namespace Freelance.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _usersRepository.Get();
        }

        public async Task<Guid> CreateUsers(User user)
        {
            return await _usersRepository.Create(user);
        }

        public async Task<Guid> UpdateUsers(Guid guid, string name, string discription, string resume, string password)
        {
            return await _usersRepository.Update(guid, name, discription, resume, password);
        }
    }
}
