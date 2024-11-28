using Freelance.Core.Abstraction;
using Freelance.Core.Models;
using Freelance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Freelance.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository 
    {
        private readonly FreelanceDatabaseContext _context;

        public UsersRepository(FreelanceDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<User>> Get()
        {
            List<UserEntity> userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            List<User> users = userEntities
                .Select(u => User.Create(u.Id, u.Name, u.Description, u.Resume, u.Photo, u.Password, u.Login).Value)
                .ToList();

            return users;
        }

    }
}
