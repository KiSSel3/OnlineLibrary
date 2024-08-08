using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Configurations;

public class GenreEntityConfiguration : BaseEntityConfiguration<GenreEntity>
{
    public override void Configure(EntityTypeBuilder<GenreEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
        builder.Property(g => g.IsDeleted).IsRequired();
    }
}