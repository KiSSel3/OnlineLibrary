namespace OnlineLibrary.Domain.Entities;

public class BookEntity : BaseEntity
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid GenreId { get; set; }
    public Guid AuthorId { get; set; }
}