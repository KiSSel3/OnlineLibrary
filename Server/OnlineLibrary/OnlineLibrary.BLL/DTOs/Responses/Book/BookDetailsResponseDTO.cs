using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.DTOs.Responses.Book;

public class BookDetailsResponseDTO
{
    public BookDTO BookDTO { get; set; }
    public GenreDTO GenreDTO { get; set; }
    public AuthorDTO AuthorDTO { get; set; }
    
    public byte[] Image { get; set; }
}