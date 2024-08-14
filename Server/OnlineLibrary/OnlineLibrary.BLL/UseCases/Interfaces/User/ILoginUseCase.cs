using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.DTOs.Responses.Other;
using OnlineLibrary.BLL.DTOs.Responses.User;

namespace OnlineLibrary.BLL.UseCases.Interfaces.User;

public interface ILoginUseCase
{
    Task<TokenResponseDTO> ExecuteAsync(UserRequestDTO userRequestDto, CancellationToken cancellationToken = default);
}