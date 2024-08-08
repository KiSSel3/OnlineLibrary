using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Configurations;

public class AuthorEntityConfiguration : BaseEntityConfiguration<AuthorEntity>
{
    public override void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.LastName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.DateOfBirth).IsRequired();
        builder.Property(a => a.Country).HasMaxLength(100);
        builder.Property(a => a.IsDeleted).IsRequired();
    }
}