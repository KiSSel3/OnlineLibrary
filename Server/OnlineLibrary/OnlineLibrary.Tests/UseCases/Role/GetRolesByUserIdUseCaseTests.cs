using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.UseCases.Implementation.Role;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Role;

public class GetRolesByUserIdUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetRolesByUserIdUseCase _useCase;

    public GetRolesByUserIdUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _mapperMock = new Mock<IMapper>();

        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IRoleRepository>()).Returns(_roleRepositoryMock.Object);
        _useCase = new GetRolesByUserIdUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnRoleDTOList()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var roles = new List<RoleEntity> { new RoleEntity() };
        var roleDTOs = new List<RoleDTO> { new RoleDTO() };

        _roleRepositoryMock.Setup(r => r.GetRolesByUserIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(roles);
        _mapperMock.Setup(m => m.Map<IEnumerable<RoleDTO>>(roles)).Returns(roleDTOs);

        // Act
        var result = await _useCase.ExecuteAsync(userId, CancellationToken.None);

        // Assert
        Assert.Equal(roleDTOs.Count, result.Count());
    }
}