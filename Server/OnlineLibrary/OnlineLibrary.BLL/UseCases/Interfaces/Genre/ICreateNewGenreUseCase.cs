namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface ICreateNewGenreUseCase
{
    Task ExecuteAsync(string genreName, CancellationToken cancellationToken = default);
}