using System.Security.Claims;
using Moq;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.Services.Interfaces;
using OnlineLibrary.BLL.UseCases.Implementation.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.User;

public class RefreshTokenUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly RefreshTokenUseCase _useCase;

    public RefreshTokenUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _tokenServiceMock = new Mock<ITokenService>();
        _useCase = new RefreshTokenUseCase(_unitOfWorkMock.Object, _tokenServiceMock.Object);
    }
    
    [Fact]
    public async Task ExecuteAsync_ShouldThrowAuthenticationException_WhenRefreshTokenIsExpired()
    {
        // Arrange
        var refreshToken = "expired_refresh_token";
        var user = new UserEntity
        {
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(-1)
        };

        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IUserRepository>().GetByRefreshTokenAsync(refreshToken, It.IsAny<CancellationToken>())).ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<AuthenticationException>(() => _useCase.ExecuteAsync(refreshToken, CancellationToken.None));
    }
}