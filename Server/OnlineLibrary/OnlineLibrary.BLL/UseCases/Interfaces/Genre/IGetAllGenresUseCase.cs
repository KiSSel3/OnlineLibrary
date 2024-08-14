using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface IGetAllGenresUseCase
{
    Task<IEnumerable<GenreDTO>> ExecuteAsync(CancellationToken cancellationToken = default);
}