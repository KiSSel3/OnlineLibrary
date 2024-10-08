using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Responses.Book;

public class BookResponseDTO : BookDTO
{
    public Guid Id { get; set; }
    public string Image { get; set; }
}