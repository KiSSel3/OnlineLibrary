using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.DTOs.Responses.Genre;

namespace OnlineLibrary.BLL.DTOs.Responses.Book;

public class BookDetailsResponseDTO
{
    public BookResponseDTO BookResponseDTO { get; set; }
    public GenreResponseDTO GenreDTO { get; set; }
    public AuthorResponseDTO AuthorResponseDTO { get; set; }
    
    public string Image { get; set; }
}