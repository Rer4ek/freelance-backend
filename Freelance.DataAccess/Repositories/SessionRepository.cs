using CSharpFunctionalExtensions;
using Freelance.Core.Abstraction;
using Freelance.Core.Models;
using Freelance.Core.Utils;
using Freelance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Freelance.DataAccess.Repositories
{
    public class SessionRepository : ISessionRepository
    {

        private readonly FreelanceDatabaseContext _context;

        public SessionRepository(FreelanceDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(User user)
        {
            SessionEntity sessionEntity = new SessionEntity()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id
            };

            await _context.AddAsync(sessionEntity);
            await _context.SaveChangesAsync();

            return sessionEntity.Id;
        }


        public async Task<Result<Guid>> GetId(User user)
        {
            List<SessionEntity> sessions = await _context.Sessions
                .AsNoTracking()
                .ToListAsync();

            SessionEntity? sessionEntity = sessions.FirstOrDefault(s => s?.UserId == user.Id, null);
            if (sessionEntity == null)
            {
                return Result.Failure<Guid>("Сессии не существует");
            }
            return Result.Success(sessionEntity.Id);
        }

        public async Task<Result<Session>> Get(User user)
        {
            List<SessionEntity> sessions = await _context.Sessions
                .AsNoTracking()
                .ToListAsync();

            SessionEntity? sessionEntity = sessions.FirstOrDefault(s => s?.UserId == user.Id, null);
            if (sessionEntity == null)
            {
                return Result.Failure<Session>("Сессии не существует");
            }
            return Result.Success(Session.Create(sessionEntity.Id, sessionEntity.UserId));
        }

        public async Task<Result> Delete(string sessionHash)
        {
            List<SessionEntity> sessions = await _context.Sessions
                .AsNoTracking()
                .ToListAsync();

            try
            {
                SessionEntity? sessionEntity = sessions
                    .FirstOrDefault(
                        s => Encryption.Compare(sessionHash, s?.Id.ToString()), null);

                if (sessionEntity == null)
                {
                    return Result.Failure("Такой сессии нету");
                }

                _context.Remove(sessionEntity);

                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                return Result.Failure(exception.Message);
            }
            return Result.Success();
        }

        public async Task<bool> IsValid(string sessionHash)
        {
            List<SessionEntity> sessions = await _context.Sessions
                .AsNoTracking()
                .ToListAsync();

            SessionEntity? sessionEntity = sessions
                .FirstOrDefault(
                s => Encryption.Compare(sessionHash, s?.Id.ToString()), null);

            if (sessionEntity == null)
            {
                return false;
            }

            return true;
        }
    }
}
