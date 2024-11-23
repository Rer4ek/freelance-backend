using Freelance.Core.Models;
using Freelance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freelance.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(User.MAX_NAME_LENGHT);

            builder.Property(u => u.Discription)
                .HasMaxLength(User.MAX_DISCRIPTION_LENGHT);

            builder.Property(u => u.Password)
                .HasMaxLength(User.MAX_PASSWORD_LENGHT);

            builder.Property(u => u.Resume);
        
        }
    }
}
