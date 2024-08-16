using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface IUpdateGenreUseCase
{
    Task ExecuteAsync(GenreDTO genreDto, CancellationToken cancellationToken = default);
}