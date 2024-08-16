using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.User;

public class RevokeUseCase : IRevokeUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public RevokeUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var userRepository = _unitOfWork.GetCustomRepository<IUserRepository>();
        
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            throw new EntityNotFoundException("User", userId);
        }

        user.RefreshToken = string.Empty;
        user.RefreshTokenExpiryTime = DateTime.MinValue;

        userRepository.Update(user);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}