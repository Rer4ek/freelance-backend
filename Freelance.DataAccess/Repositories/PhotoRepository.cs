using CSharpFunctionalExtensions;
using Freelance.Core.Models;
using Freelance.Core.Abstraction;
using Freelance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Freelance.DataAccess.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly FreelanceDatabaseContext _context;

        public PhotoRepository(FreelanceDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Guid?> Update(ClientFile? clientFile)
        {

            if (clientFile == null)
            {
                return null;
            }

            int count = _context.Photos.Where(p => p.Id == clientFile.Id).Count();
            if (count == 0)
            {
                return await Create(clientFile);
            }

            await _context.Photos
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

            PhotoFileEntity photo = new PhotoFileEntity
            {
                Id = clientFile.Id,
                Path = clientFile.Path,
            };
            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();

            return photo.Id;
        }
    }
}
