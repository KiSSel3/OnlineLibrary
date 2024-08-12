using OnlineLibrary.BLL.DTOs.Responses.Author;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Author;

public interface IGetAllAuthorsUseCase
{
    Task<IEnumerable<AuthorResponseDTO>> ExecuteAsync(CancellationToken cancellationToken = default);
}