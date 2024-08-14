using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Responses.Author;

public class AuthorResponseDTO
{
    public AuthorDTO AuthorDTO { get; set; }
    public Guid Id { get; set; }
}