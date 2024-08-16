using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.UseCases.Implementation.Role;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.Tests.UseCases.Role;

public class CheckUserHasRoleUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly CheckUserHasRoleUseCase _useCase;

    public CheckUserHasRoleUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IRoleRepository>()).Returns(_roleRepositoryMock.Object);
        _useCase = new CheckUserHasRoleUseCase(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnTrue_WhenUserHasRole()
    {
        // Arrange
        var userRoleDTO = new UserRoleDTO { UserId = Guid.NewGuid(), RoleId = Guid.NewGuid() };
        _roleRepositoryMock.Setup(r => r.CheckUserHasRoleAsync(userRoleDTO.UserId, userRoleDTO.RoleId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _useCase.ExecuteAsync(userRoleDTO, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFalse_WhenUserDoesNotHaveRole()
    {
        // Arrange
        var userRoleDTO = new UserRoleDTO { UserId = Guid.NewGuid(), RoleId = Guid.NewGuid() };
        _roleRepositoryMock.Setup(r => r.CheckUserHasRoleAsync(userRoleDTO.UserId, userRoleDTO.RoleId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _useCase.ExecuteAsync(userRoleDTO, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}