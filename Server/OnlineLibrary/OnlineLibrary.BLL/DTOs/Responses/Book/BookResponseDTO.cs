using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Responses.Book;

public class BookResponseDTO
{
    public BookDTO BookDTO { get; set; }
    
    public byte[] Image { get; set; }
    
    public Guid Id { get; set; }
}