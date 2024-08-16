using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.BLL.UseCases.Implementation.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.User;

public class GetAllUsersUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllUsersUseCase _useCase;

    public GetAllUsersUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _useCase = new GetAllUsersUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnUserResponseDTOList()
    {
        // Arrange
        var users = new List<UserEntity> { new UserEntity() };
        var userDTOs = new List<UserResponseDTO> { new UserResponseDTO() };

        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IUserRepository>().GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(users);
        _mapperMock.Setup(m => m.Map<IEnumerable<UserResponseDTO>>(users)).Returns(userDTOs);

        // Act
        var result = await _useCase.ExecuteAsync(CancellationToken.None);

        // Assert
        Assert.Equal(userDTOs.Count, result.Count());
    }
}