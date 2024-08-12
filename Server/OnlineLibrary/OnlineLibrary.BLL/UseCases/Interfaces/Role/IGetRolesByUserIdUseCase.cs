using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Role;

public interface IGetRolesByUserIdUseCase
{
    Task<IEnumerable<RoleDTO>> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default);
}