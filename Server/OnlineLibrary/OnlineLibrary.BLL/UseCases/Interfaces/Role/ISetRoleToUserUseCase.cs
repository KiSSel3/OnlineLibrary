using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Role;

public interface ISetRoleToUserUseCase
{
    Task ExecuteAsync(UserRoleDTO userRoleDTO, CancellationToken cancellationToken = default);
}