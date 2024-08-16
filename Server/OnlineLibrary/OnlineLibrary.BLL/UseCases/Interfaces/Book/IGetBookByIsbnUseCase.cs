using OnlineLibrary.BLL.DTOs.Responses.Book;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Book;

public interface IGetBookByIsbnUseCase
{
    Task<BookDetailsResponseDTO> ExecuteAsync(string isbn, CancellationToken cancellationToken = default);
}