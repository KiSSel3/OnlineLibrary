using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Responses.Author;

public class AuthorResponseDTO
{
    public Guid Id { get; set; }
    public AuthorDTO AuthorDTO { get; set; }
}