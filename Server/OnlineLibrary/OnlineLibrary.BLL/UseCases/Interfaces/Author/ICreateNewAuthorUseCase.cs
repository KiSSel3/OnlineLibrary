using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Author;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Author;

public interface ICreateNewAuthorUseCase
{
    Task ExecuteAsync(AuthorDTO authorDTO, CancellationToken cancellationToken = default);
}