using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface IGetAllGenresUseCase
{
    Task<IEnumerable<GenreCommonDTO>> ExecuteAsync(CancellationToken cancellationToken = default);
}