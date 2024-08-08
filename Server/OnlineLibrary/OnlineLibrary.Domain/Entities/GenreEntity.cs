namespace OnlineLibrary.Domain.Entities;

public class GenreEntity : BaseEntity
{
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}