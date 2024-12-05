using Freelance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freelance.DataAccess.Configurations
{
    public class ResumeFileConfiguration : IEntityTypeConfiguration<ResumeFileEntity>
    {
        public void Configure(EntityTypeBuilder<ResumeFileEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Path)
                .IsRequired();

            builder
                .HasOne<UserEntity>()
                .WithOne(u => u.Resume);
        }
    }
}
