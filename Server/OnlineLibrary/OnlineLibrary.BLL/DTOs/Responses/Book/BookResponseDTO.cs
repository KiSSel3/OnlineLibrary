using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Responses.Book;

public class BookResponseDTO
{
    public Guid Id { get; set; }
    public BookDTO BookDTO { get; set; }
    
    public string Image { get; set; }
}