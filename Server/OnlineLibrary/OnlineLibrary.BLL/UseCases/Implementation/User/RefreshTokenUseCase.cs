using OnlineLibrary.BLL.DTOs.Responses.Other;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.BLL.UseCases.Interfaces.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.User;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public RefreshTokenUseCase(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<TokenResponseDTO> ExecuteAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var user = await GetUserByRefreshTokenAsync(refreshToken, cancellationToken);
        ValidateRefreshToken(user);
        
        var accessToken = await GenerateAccessTokenAsync(user, cancellationToken);

        return new TokenResponseDTO() { RefreshToken = refreshToken, AccessToken = accessToken };
    }

    private async Task<UserEntity> GetUserByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var userRepository = _unitOfWork.GetCustomRepository<IUserRepository>();
        
        var user = await userRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);
        if (user == null)
        {
            throw new AuthenticationException("Invalid refresh token.");
        }

        return user;
    }

    private void ValidateRefreshToken(UserEntity user)
    {
        var isCorrect = user.RefreshTokenExpiryTime > DateTime.UtcNow;
        if (!isCorrect)
        {
            throw new AuthenticationException("Refresh token has expired.");
        }
    }

    private async Task<string> GenerateAccessTokenAsync(UserEntity user, CancellationToken cancellationToken)
    {
        var rolesByUser = await _unitOfWork.GetCustomRepository<IRoleRepository>().GetRolesByUserIdAsync(user.Id, cancellationToken);
        var claims = _tokenService.CreateClaims(user, rolesByUser.ToList());
        return _tokenService.GenerateAccessToken(claims);
    }
}
