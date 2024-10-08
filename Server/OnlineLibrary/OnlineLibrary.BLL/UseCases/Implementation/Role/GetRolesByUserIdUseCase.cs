using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Role;
using OnlineLibrary.BLL.UseCases.Interfaces.Role;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.Role;

public class GetRolesByUserIdUseCase : IGetRolesByUserIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRolesByUserIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleResponseDTO>> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var roles = await _unitOfWork.GetCustomRepository<IRoleRepository>().GetRolesByUserIdAsync(userId, cancellationToken);
        return _mapper.Map<IEnumerable<RoleResponseDTO>>(roles);
    }
}