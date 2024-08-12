using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface IGetGenreByIdUseCase
{
    Task<GenreCommonDTO> ExecuteAsync(Guid genreId, CancellationToken cancellationToken = default);
}