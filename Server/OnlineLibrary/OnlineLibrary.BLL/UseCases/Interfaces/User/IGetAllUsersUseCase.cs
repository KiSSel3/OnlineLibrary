using OnlineLibrary.BLL.DTOs.Responses.User;

namespace OnlineLibrary.BLL.UseCases.Interfaces.User;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<UserResponseDTO>> ExecuteAsync(CancellationToken cancellationToken = default);
}