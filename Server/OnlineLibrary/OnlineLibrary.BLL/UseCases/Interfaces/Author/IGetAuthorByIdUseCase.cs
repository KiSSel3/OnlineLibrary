using OnlineLibrary.BLL.DTOs.Responses.Author;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Author;

public interface IGetAuthorByIdUseCase
{
    Task<AuthorResponseDTO> ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default);
}