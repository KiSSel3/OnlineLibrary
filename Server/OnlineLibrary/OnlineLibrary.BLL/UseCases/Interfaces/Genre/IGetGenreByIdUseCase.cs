using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Genre;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface IGetGenreByIdUseCase
{
    Task<GenreResponseDTO> ExecuteAsync(Guid genreId, CancellationToken cancellationToken = default);
}