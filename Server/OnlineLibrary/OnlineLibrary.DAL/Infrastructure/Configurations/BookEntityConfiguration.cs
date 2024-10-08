using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Configurations;

public class BookEntityConfiguration : BaseEntityConfiguration<BookEntity>
{
    public override void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(b => b.ISBN).IsRequired().HasMaxLength(13);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Description).HasMaxLength(1000);
        builder.Property(b => b.GenreId).IsRequired();
        builder.Property(b => b.AuthorId).IsRequired();
        builder.Property(b => b.IsDeleted).IsRequired();
        
        builder.Property(e => e.Image).IsRequired(false).HasColumnType("bytea");
    }
}