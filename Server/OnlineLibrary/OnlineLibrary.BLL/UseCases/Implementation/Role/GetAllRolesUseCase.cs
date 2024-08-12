using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.UseCases.Interfaces.Role;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.Role;

public class GetAllRolesUseCase : IGetAllRolesUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllRolesUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleDTO>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _unitOfWork.GetCustomRepository<IRoleRepository>().GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<RoleDTO>>(roles);
    }
}