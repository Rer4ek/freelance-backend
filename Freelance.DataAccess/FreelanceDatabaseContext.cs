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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
