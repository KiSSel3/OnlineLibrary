using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Author;

public interface ICreateNewAuthorUseCase
{
    Task ExecuteAsync(AuthorDTO authorDTO, CancellationToken cancellationToken = default);
}