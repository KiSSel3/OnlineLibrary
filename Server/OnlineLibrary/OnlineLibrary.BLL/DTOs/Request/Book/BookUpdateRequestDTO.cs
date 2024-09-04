using Microsoft.AspNetCore.Http;
using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Request.Book;

public class BookUpdateRequestDTO : BookDTO
{ 
    public IFormFile? Image { get; set; }
    
    public Guid GenreId { get; set; }
    public Guid AuthorId { get; set; }
}