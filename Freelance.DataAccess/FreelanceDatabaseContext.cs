using Freelance.DataAccess.Configurations;
using Freelance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal;

namespace Freelance.DataAccess
{
    public class FreelanceDatabaseContext: DbContext
    {   
        public FreelanceDatabaseContext(DbContextOptions<FreelanceDatabaseContext> option) : base(option)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }

        public DbSet<PhotoFileEntity> Photos { get; set; }

        public DbSet<ResumeFileEntity> Resumes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoFileConfiguration());
            modelBuilder.ApplyConfiguration(new ResumeFileConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
