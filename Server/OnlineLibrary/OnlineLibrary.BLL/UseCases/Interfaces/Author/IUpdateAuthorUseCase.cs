using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Author;

public interface IUpdateAuthorUseCase
{
    Task ExecuteAsync(Guid authorId, AuthorDTO authorDTO, CancellationToken cancellationToken = default);
}