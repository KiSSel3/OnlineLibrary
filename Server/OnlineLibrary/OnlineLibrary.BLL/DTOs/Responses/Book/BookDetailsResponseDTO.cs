using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Author;

namespace OnlineLibrary.BLL.DTOs.Responses.Book;

public class BookDetailsResponseDTO
{
    public BookResponseDTO BookResponseDTO { get; set; }
    public GenreDTO GenreDTO { get; set; }
    public AuthorResponseDTO AuthorResponseDTO { get; set; }
    
    public string Image { get; set; }
}