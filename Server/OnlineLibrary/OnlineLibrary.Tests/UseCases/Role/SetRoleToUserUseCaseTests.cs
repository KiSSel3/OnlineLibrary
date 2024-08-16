using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Role;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Role;

public class SetRoleToUserUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly SetRoleToUserUseCase _useCase;

    public SetRoleToUserUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _mapperMock = new Mock<IMapper>();

        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IUserRepository>()).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IRoleRepository>()).Returns(_roleRepositoryMock.Object);
        _useCase = new SetRoleToUserUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenUserNotFound()
    {
        // Arrange
        var userRoleDTO = new UserRoleDTO { UserId = Guid.NewGuid(), RoleId = Guid.NewGuid() };
        _userRepositoryMock.Setup(r => r.GetByIdAsync(userRoleDTO.UserId, It.IsAny<CancellationToken>())).ReturnsAsync((UserEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(userRoleDTO, CancellationToken.None));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenRoleNotFound()
    {
        // Arrange
        var userRoleDTO = new UserRoleDTO { UserId = Guid.NewGuid(), RoleId = Guid.NewGuid() };
        _userRepositoryMock.Setup(r => r.GetByIdAsync(userRoleDTO.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(new UserEntity());
        _roleRepositoryMock.Setup(r => r.GetByIdAsync(userRoleDTO.RoleId, It.IsAny<CancellationToken>())).ReturnsAsync((RoleEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(userRoleDTO, CancellationToken.None));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldSetRoleToUserAndSave_WhenUserAndRoleExist()
    {
        // Arrange
        var userRoleDTO = new UserRoleDTO { UserId = Guid.NewGuid(), RoleId = Guid.NewGuid() };
        var user = new UserEntity();
        var role = new RoleEntity();
        var userRoleEntity = new UserRoleEntity();

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userRoleDTO.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
        _roleRepositoryMock.Setup(r => r.GetByIdAsync(userRoleDTO.RoleId, It.IsAny<CancellationToken>())).ReturnsAsync(role);
        _mapperMock.Setup(m => m.Map<UserRoleEntity>(userRoleDTO)).Returns(userRoleEntity);

        // Act
        await _useCase.ExecuteAsync(userRoleDTO, CancellationToken.None);

        // Assert
        _roleRepositoryMock.Verify(r => r.SetRoleToUserAsync(userRoleEntity, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}