using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface IGetGenreByIdUseCase
{
    Task<GenreDTO> ExecuteAsync(Guid genreId, CancellationToken cancellationToken = default);
}