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
                .Select(u => User.Create(u.Id, u.Name, u.Discription, u.Resume, u.Password).Value)
                .ToList();

            return users;
        }

        public async Task<Guid> Create(User user)
        {
            UserEntity userEntity = new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Discription = user.Discription,
                Resume = user.Resume,
                Password = user.Password
            };

            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<Guid> Update(Guid guid, string name, string discription, string resume, string password)
        {
            await _context.Users
                .Where(u => u.Id == guid)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Name, u => name)
                    .SetProperty(u => u.Discription, u => discription)
                    .SetProperty(u => u.Resume, u => resume)
                    .SetProperty(u => u.Password, u => password)
                );

            return guid;
        }

    }
}
