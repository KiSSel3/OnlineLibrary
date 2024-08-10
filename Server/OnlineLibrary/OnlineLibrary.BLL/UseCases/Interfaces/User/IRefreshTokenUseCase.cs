using OnlineLibrary.BLL.DTOs.Responses.User;

namespace OnlineLibrary.BLL.UseCases.Interfaces.User;

public interface IRefreshTokenUseCase
{
    Task<TokenResponseDTO> ExecuteAsync(string refreshToken, CancellationToken cancellationToken = default);
}