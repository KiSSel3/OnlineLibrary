using MapsterMapper;
using Microsoft.Extensions.Configuration;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.DTOs.Responses.Other;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.BLL.UseCases.Interfaces.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.User;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;
    private readonly IConfiguration _configuration;

    public LoginUseCase(IUnitOfWork unitOfWork, ITokenService tokenService, IPasswordService passwordService, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _passwordService = passwordService;
        _configuration = configuration;
    }

 public async Task<TokenResponseDTO> ExecuteAsync(LoginRequestDTO loginRequestDto, CancellationToken cancellationToken = default)
    {
        var user = await GetUserAsync(loginRequestDto.Login, cancellationToken);
        VerifyPassword(user.PasswordHash, loginRequestDto.Password);
        
        var refreshToken = GenerateRefreshToken(user);
        var accessToken = await GenerateAccessTokenAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new TokenResponseDTO() { RefreshToken = refreshToken, AccessToken = accessToken };
    }

    private async Task<UserEntity> GetUserAsync(string login, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetCustomRepository<IUserRepository>();
        
        var user = await repository.GetByLoginAsync(login, cancellationToken);
        if (user is null)
        {
            throw new AuthenticationException("Login or password entered incorrectly.");
        }

        return user;
    }

    private void VerifyPassword(string storedPasswordHash, string providedPassword)
    {
        var isCorrect = _passwordService.VerifyPassword(storedPasswordHash, providedPassword);
        if (!isCorrect)
        {
            throw new AuthenticationException("Login or password entered incorrectly.");
        }
    }

    private string GenerateRefreshToken(UserEntity user)
    {
        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("Jwt:RefreshTokenExpirationDays").Get<int>());

        var repository = _unitOfWork.GetCustomRepository<IUserRepository>();
        repository.Update(user);

        return refreshToken;
    }

    private async Task<string> GenerateAccessTokenAsync(UserEntity user, CancellationToken cancellationToken)
    {
        var rolesByUser = await _unitOfWork.GetCustomRepository<IRoleRepository>().GetRolesByUserIdAsync(user.Id, cancellationToken);
        var claims = _tokenService.CreateClaims(user, rolesByUser.ToList());
        return _tokenService.GenerateAccessToken(claims);
    }
}