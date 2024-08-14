using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Configurations;

public class UserRoleEntityConfiguration : BaseEntityConfiguration<UserRoleEntity>
{
    public override void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(ur => ur.UserId).IsRequired();
        builder.Property(ur => ur.RoleId).IsRequired();
        builder.Property(ur => ur.IsDeleted).IsRequired();
    }
}