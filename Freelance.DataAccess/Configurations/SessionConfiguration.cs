using Freelance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freelance.DataAccess.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<SessionEntity>
    {
        public void Configure(EntityTypeBuilder<SessionEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder
                .Property(s => s.UserId)
                .IsRequired();

            builder
                .HasOne<UserEntity>()
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId);
        }
    }
}
