namespace OnlineLibrary.Domain.Entities;

public class LoanEntity : BaseEntity
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateTime BorrowedAt { get; set; }
    public DateTime ReturnBy { get; set; }
}