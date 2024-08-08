using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Configurations;

public class LoanEntityConfiguration : BaseEntityConfiguration<LoanEntity>
{
    public override void Configure(EntityTypeBuilder<LoanEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(l => l.BookId).IsRequired();
        builder.Property(l => l.UserId).IsRequired();
        builder.Property(l => l.BorrowedAt).IsRequired();
        builder.Property(l => l.ReturnBy).IsRequired();
        builder.Property(l => l.IsDeleted).IsRequired();
        
        //TODO: Delete or Fix
        /*builder.HasOne<BookEntity>()
            .WithMany()
            .HasForeignKey(l => l.BookId);
        
        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(l => l.UserId);*/
    }
}