namespace OnlineLibrary.BLL.UseCases.Interfaces.Book;

public interface IDeleteBookUseCase
{
    Task ExecuteAsync(Guid bookId, CancellationToken cancellationToken = default);
}