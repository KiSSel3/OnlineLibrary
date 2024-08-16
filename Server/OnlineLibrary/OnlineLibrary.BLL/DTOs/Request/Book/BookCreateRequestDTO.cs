using Microsoft.AspNetCore.Http;
using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Request.Book;

public class BookCreateRequestDTO
{
    public BookDTO BookDTO { get; set; }
    
    public IFormFile? Image { get; set; }
    
    public Guid GenreId { get; set; }
    public Guid AuthorId { get; set; }
}