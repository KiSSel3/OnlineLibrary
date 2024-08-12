using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Request.Author;

public class AuthorUpdateRequestDTO
{
    public AuthorDTO AuthorDTO { get; set; }
    public Guid Id { get; set; }
}