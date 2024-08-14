using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Role;

public interface IGetAllRolesUseCase
{
    Task<IEnumerable<RoleDTO>> ExecuteAsync(CancellationToken cancellationToken = default);
}