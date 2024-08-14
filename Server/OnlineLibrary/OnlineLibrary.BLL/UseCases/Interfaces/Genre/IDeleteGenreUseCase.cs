namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface IDeleteGenreUseCase
{
    Task ExecuteAsync(Guid genreId, CancellationToken cancellationToken = default);
}