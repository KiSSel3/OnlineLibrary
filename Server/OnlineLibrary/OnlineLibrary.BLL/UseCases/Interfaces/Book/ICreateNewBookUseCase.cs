using OnlineLibrary.BLL.DTOs.Request.Book;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Book;

public interface ICreateNewBookUseCase
{
    Task ExecuteAsync(BookCreateRequestDTO bookRequestDTO, CancellationToken cancellationToken = default);
}