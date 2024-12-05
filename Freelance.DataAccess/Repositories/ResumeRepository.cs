using CSharpFunctionalExtensions;
using Freelance.Core.Models;
using Freelance.Core.Abstraction;
using Freelance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Freelance.DataAccess.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly FreelanceDatabaseContext _context;

        public ResumeRepository(FreelanceDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Guid?> Update(ClientFile? clientFile)
        {

            if (clientFile == null)
            {
                return null;
            }

            int count = _context.Resumes.Where(p => p.Id == clientFile.Id).Count();
            if (count == 0)
            {
                return await Create(clientFile);
            }

            await _context.Resumes
                .Where(p => p.Id == clientFile.Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.Path, clientFile.Path)
            );

            return clientFile.Id;
        }

        public async Task<Guid?> Create(ClientFile? clientFile)
        {
            if (clientFile == null)
            {
                return null;
            }

            ResumeFileEntity resume = new ResumeFileEntity
            {
                Id = clientFile.Id,
                Path = clientFile.Path,
            };
            await _context.Resumes.AddAsync(resume);
            await _context.SaveChangesAsync();

            return resume.Id;
        }
    }
}
