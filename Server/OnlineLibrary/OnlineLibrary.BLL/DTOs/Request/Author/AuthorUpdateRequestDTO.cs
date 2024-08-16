using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Request.Author;

public class AuthorUpdateRequestDTO
{
    public Guid Id { get; set; }
    public AuthorDTO AuthorDTO { get; set; }
}