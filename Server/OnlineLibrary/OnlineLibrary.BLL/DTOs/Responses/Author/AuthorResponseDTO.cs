namespace OnlineLibrary.BLL.DTOs.Responses.Author;

public class AuthorResponseDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Country { get; set; }
}