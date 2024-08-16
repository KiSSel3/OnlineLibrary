using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Domain.Models;

public class NotificationEventArgs
{
    public UserEntity User { get; set; }
    public BookEntity Book { get; set; }
    public LoanEntity Loan { get; set; }
}