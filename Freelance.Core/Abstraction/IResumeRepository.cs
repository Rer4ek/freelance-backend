using Freelance.Core.Models;

namespace Freelance.Core.Abstraction
{
    public interface IResumeRepository
    {
        Task<Guid?> Create(ClientFile? clientFile);
        Task<Guid?> Update(ClientFile? clientFile);
    }
}