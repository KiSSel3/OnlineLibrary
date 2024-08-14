namespace OnlineLibrary.BLL.UseCases.Interfaces.Author;

public interface IDeleteAuthorUseCase
{
    Task ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default);
}