using OnlineLibrary.BLL.DTOs.Request.Author;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Author;

public interface ICreateNewAuthorUseCase
{
    Task ExecuteAsync(AuthorCreateRequestDTO authorRequestDTO, CancellationToken cancellationToken = default);
}