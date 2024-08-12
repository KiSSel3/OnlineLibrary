namespace OnlineLibrary.BLL.DTOs.Request.Author;

public class AuthorCreateRequestDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Country { get; set; }
}