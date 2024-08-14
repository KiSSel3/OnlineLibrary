namespace OnlineLibrary.BLL.DTOs.Request.Book;

public class BookParametersRequestDTO
{
    public string? SearchName { get; set; }
    
    public Guid? GenreId { get; set; }
    public Guid? AuthorId { get; set; }
    
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}