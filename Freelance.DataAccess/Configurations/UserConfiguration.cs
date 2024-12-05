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
                .HasMaxLength(User.MAX_NAME_LENGHT)
                .HasDefaultValue("Редиска582");

            builder.Property(u => u.Description)
                .HasMaxLength(User.MAX_Description_LENGHT);

            builder.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(User.MAX_NAME_LENGHT);

            builder.HasIndex(u => u.Login)
                .IsUnique();

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.Description)
                .HasMaxLength(User.MAX_Description_LENGHT)
                .HasDefaultValue(string.Empty);

            builder
                .HasMany(s => s.Sessions)
                .WithOne();

            builder
                .HasOne(u => u.Photo)
                .WithOne()
                .HasForeignKey<UserEntity>(u => u.PhotoId);

            builder
                .HasOne(u => u.Resume)
                .WithOne()
                .HasForeignKey<UserEntity>(u => u.ResumeId);
        }
    }
}
