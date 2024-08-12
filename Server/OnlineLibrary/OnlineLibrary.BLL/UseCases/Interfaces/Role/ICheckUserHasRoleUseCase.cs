using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Role;

public interface ICheckUserHasRoleUseCase
{
    Task<bool> ExecuteAsync(UserRoleDTO userRoleDTO, CancellationToken cancellationToken = default);
}