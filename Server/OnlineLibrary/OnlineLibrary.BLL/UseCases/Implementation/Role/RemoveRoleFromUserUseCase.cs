using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Role;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.Role;

public class RemoveRoleFromUserUseCase : IRemoveRoleFromUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveRoleFromUserUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(UserRoleDTO userRoleDTO, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.GetCustomRepository<IUserRepository>()
            .GetByIdAsync(userRoleDTO.UserId, cancellationToken);
        
        if (user == null)
        {
            throw new EntityNotFoundException("User", userRoleDTO.UserId);
        }

        var role = await _unitOfWork.GetCustomRepository<IRoleRepository>()
            .GetByIdAsync(userRoleDTO.RoleId, cancellationToken);
        
        if (role == null)
        {
            throw new EntityNotFoundException("Role", userRoleDTO.RoleId);
        }

        await _unitOfWork.GetCustomRepository<IRoleRepository>()
            .RemoveRoleFromUserAsync(userRoleDTO.UserId, userRoleDTO.RoleId, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}