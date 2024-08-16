using Moq;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.User;

public class RevokeUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly RevokeUseCase _useCase;

    public RevokeUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _useCase = new RevokeUseCase(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldClearUserTokens_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new UserEntity { Id = userId, RefreshToken = "old_refresh_token", RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1) };

        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IUserRepository>().GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(user);

        // Act
        await _useCase.ExecuteAsync(userId, CancellationToken.None);

        // Assert
        Assert.Empty(user.RefreshToken);
        Assert.Equal(DateTime.MinValue, user.RefreshTokenExpiryTime);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IUserRepository>().GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync((UserEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(userId, CancellationToken.None));
    }
}