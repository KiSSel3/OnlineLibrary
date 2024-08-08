using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Configurations;

public class RoleEntityConfiguration : BaseEntityConfiguration<RoleEntity>
{
    public override void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
        builder.Property(r => r.IsDeleted).IsRequired();
    }
}