using CSharpFunctionalExtensions;
using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface ISessionRepository
    {
        Task<Guid> Create(User user);
        Task<Result> Delete(User user);
        Task<Result<Session>> Get(User user);
        Task<Result<Guid>> GetId(User user);
        Task<bool> IsValid(Guid sessionId);
        Task<bool> IsValid(Session session);
    }
}