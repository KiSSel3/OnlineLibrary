using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Genre;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Genre;

public interface IUpdateGenreUseCase
{
    Task ExecuteAsync(Guid genreId, string genreName, CancellationToken cancellationToken = default);
}