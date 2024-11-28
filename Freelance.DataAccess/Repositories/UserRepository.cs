using Freelance.Core.Models;
using Freelance.Core.Abstraction;
using Freelance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Freelance.Core.Utils;

namespace Freelance.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly FreelanceDatabaseContext _context;

        public UserRepository(FreelanceDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(User user)
        {
            string password = Encryption.Encrypt(user.Password);
            UserEntity userEntity = new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Description = user.Description,
                Resume = user.Resume,
                Photo = user.Photo,
                Password = password,
                Login = user.Login,
            };

            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<User?> GetByLogin(string login)
        {
            List<UserEntity> users = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            User? user = users
                .Select(u => User.Create(u.Id, u.Name, u.Description, u.Resume, u.Photo, u.Password, u.Login).Value)
                .FirstOrDefault(u => u?.Login == login, null);

            return user;
        }

        public async Task<Guid> Update(Guid guid, string? name, string? description, string? resume, string? photo, string password, string login)
        {
            await _context.Users
                .Where(u => u.Id == guid)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Name, u => name)
                    .SetProperty(u => u.Description, u => description)
                    .SetProperty(u => u.Resume, u => resume)
                    .SetProperty(u => u.Photo, u => photo)
                    .SetProperty(u => u.Password, u => password)
                    .SetProperty(u => u.Login, u => login)
                );

            return guid;
        }
    }
}
