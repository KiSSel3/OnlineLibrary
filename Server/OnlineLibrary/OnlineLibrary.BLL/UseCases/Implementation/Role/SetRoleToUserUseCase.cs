using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Role;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Role;

public class SetRoleToUserUseCase : ISetRoleToUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SetRoleToUserUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

        var userRole = _mapper.Map<UserRoleEntity>(userRoleDTO);

        await _unitOfWork.GetCustomRepository<IRoleRepository>().SetRoleToUserAsync(userRole, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}