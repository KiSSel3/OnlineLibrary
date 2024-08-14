using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.UseCases.Interfaces.Role;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.Role;

public class CheckUserHasRoleUseCase : ICheckUserHasRoleUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckUserHasRoleUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(UserRoleDTO userRoleDTO, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.GetCustomRepository<IRoleRepository>()
            .CheckUserHasRoleAsync(userRoleDTO.UserId, userRoleDTO.RoleId, cancellationToken);
    }
}