namespace OnlineLibrary.Domain.Entities;

public class AuthorEntity : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Country { get; set; }
    public bool IsDeleted { get; set; }
}