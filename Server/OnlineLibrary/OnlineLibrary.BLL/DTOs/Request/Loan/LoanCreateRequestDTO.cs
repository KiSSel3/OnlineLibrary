namespace OnlineLibrary.BLL.DTOs.Request.Loan;

public class LoanCreateRequestDTO
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public int DayCount { get; set; }
}