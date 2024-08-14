using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.DTOs.Responses.Book;
using OnlineLibrary.BLL.Infrastructure.Helpers;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Book;

public interface IGetAllBooksUseCase
{
    Task<PagedList<BookResponseDTO>> ExecuteAsync(BookParametersRequestDTO bookParametersRequestDTO,
        CancellationToken cancellationToken = default);
}