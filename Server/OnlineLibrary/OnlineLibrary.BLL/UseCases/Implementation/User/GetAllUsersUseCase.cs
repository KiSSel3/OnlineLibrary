using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.BLL.UseCases.Interfaces.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.User;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetAllUsersUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDTO>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var users = await _unitOfWork.GetCustomRepository<IUserRepository>().GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
    }
}