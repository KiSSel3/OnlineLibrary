using OnlineLibrary.BLL.DTOs.Request.Book;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Book;

public interface IUpdateBookUseCase
{
    Task ExecuteAsync(BookUpdateRequestDTO bookRequestDTO, CancellationToken cancellationToken = default);
}