using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Role;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Role;

public interface IGetRolesByUserIdUseCase
{
    Task<IEnumerable<RoleResponseDTO>> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default);
}