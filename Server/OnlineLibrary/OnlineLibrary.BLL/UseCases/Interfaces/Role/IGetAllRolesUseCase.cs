using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Role;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Role;

public interface IGetAllRolesUseCase
{
    Task<IEnumerable<RoleResponseDTO>> ExecuteAsync(CancellationToken cancellationToken = default);
}