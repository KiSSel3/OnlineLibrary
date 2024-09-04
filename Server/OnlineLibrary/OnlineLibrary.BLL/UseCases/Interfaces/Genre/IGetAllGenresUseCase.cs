using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Genre;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface IGetAllGenresUseCase
{
    Task<IEnumerable<GenreResponseDTO>> ExecuteAsync(CancellationToken cancellationToken = default);
}