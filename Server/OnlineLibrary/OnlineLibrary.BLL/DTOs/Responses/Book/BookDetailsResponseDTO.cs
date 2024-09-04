using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.DTOs.Responses.Genre;

namespace OnlineLibrary.BLL.DTOs.Responses.Book;

public class BookDetailsResponseDTO : BookResponseDTO
{
    public GenreResponseDTO GenreResponseDTO { get; set; }
    public AuthorResponseDTO AuthorResponseDTO { get; set; }
}