using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.User;

namespace OnlineLibrary.BLL.DTOs.Responses.Loan;

public class LoanResponseDTO
{
    public UserResponseDTO UserDTO { get; set; }
    public BookDTO BookDTO { get; set; }
    
    public DateTime BorrowedAt { get; set; }
    public DateTime ReturnBy { get; set; }
}