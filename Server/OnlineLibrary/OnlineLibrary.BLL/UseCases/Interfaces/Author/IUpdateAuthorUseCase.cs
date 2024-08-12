using OnlineLibrary.BLL.DTOs.Request.Author;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Author;

public interface IUpdateAuthorUseCase
{
    Task ExecuteAsync(AuthorUpdateRequestDTO authorRequestDTO, CancellationToken cancellationToken = default);
}