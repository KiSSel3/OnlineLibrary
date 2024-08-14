using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Configurations;

public class UserEntityConfiguration : BaseEntityConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(u => u.Login).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.Property(u => u.RefreshToken).HasMaxLength(500);
        builder.Property(u => u.RefreshTokenExpiryTime).IsRequired();
        builder.Property(u => u.IsDeleted).IsRequired();
    }
}