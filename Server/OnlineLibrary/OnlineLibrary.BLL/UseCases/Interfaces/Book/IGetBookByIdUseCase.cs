using OnlineLibrary.BLL.DTOs.Responses.Book;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Book;

public interface IGetBookByIdUseCase
{
    Task<BookDetailsResponseDTO> ExecuteAsync(Guid bookId, CancellationToken cancellationToken = default);
}