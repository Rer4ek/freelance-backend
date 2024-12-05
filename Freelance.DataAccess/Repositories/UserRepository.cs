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


            ResumeFileEntity? resume = null;
            
            if (user.Resume != null)
            {
                resume = new ResumeFileEntity
                {
                    Id = user.Resume.Id,
                    Path = user.Resume.Path,
                };
            }

            PhotoFileEntity? photo = null;

            if (user.Photo != null)
            {
                photo = new PhotoFileEntity
                {
                    Id = user.Photo.Id,
                    Path = user.Photo.Path,
                };
            }

            UserEntity userEntity = new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Description = user.Description,
                ResumeId = resume?.Id,
                PhotoId = photo?.Id,
                Resume = resume,
                Photo = photo,
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
                .Include(u => u.Resume)
                .Include(u => u.Photo)
                .ToListAsync();

            User? user = users
                .Select(u => User.Create(u.Id, u.Name, u.Description,
                    CreateClientFile(u.Resume), CreateClientFile(u.Photo), u.Password, u.Login).Value)
                .FirstOrDefault(u => u?.Login == login, null);

            return user;
        }

        public async Task<Guid> Update(User user)
        {

            Guid? photoId = user.Photo?.Id;
            Guid? resumeId = user.Resume?.Id;

            await _context.Users
                .Where(u => u.Id == user.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Name, u => user.Name)
                    .SetProperty(u => u.Description, u => user.Description)
                    .SetProperty(u => u.ResumeId, u => resumeId)
                    .SetProperty(u => u.PhotoId, u => photoId)
                    .SetProperty(u => u.Password, u => Encryption.Encrypt(user.Password))
                    .SetProperty(u => u.Login, u => user.Login)
                );

            return user.Id;
        }

        public async Task<User?> GetBySession(string sessionHash)
        {
            List<SessionEntity> sessions = await _context.Sessions
                .AsNoTracking()
                .ToListAsync();

            List<UserEntity> users = await _context.Users
                .AsNoTracking()
                .Include(u => u.Resume)
                .Include(u => u.Photo)
                .ToListAsync();

            SessionEntity? session = sessions.FirstOrDefault(
                s => Encryption.Compare(sessionHash, s?.Id.ToString()), null);

            if (session == null)
            {
                return null;
            }

            UserEntity? user = users.FirstOrDefault(u => u?.Id == session.UserId, null);        

            if (user == null)
            {
                return null;
            }

            return User.Create(user.Id, user.Name, user.Description, CreateClientFile(user.Resume), CreateClientFile(user.Photo), user.Password, user.Login).Value;
        }

        private ClientFile? CreateClientFile(ClientFileEntity? entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new ClientFile(entity.Id, entity.Path);
        }
    }
}
