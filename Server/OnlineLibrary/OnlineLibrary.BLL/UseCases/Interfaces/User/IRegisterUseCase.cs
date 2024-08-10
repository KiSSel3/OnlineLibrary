using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.DTOs.Responses.User;

namespace OnlineLibrary.BLL.UseCases.Interfaces.User;

public interface IRegisterUseCase
{
    Task<TokenResponseDTO> ExecuteAsync(UserRequestDTO userRequestDto, CancellationToken cancellationToken = default);
}