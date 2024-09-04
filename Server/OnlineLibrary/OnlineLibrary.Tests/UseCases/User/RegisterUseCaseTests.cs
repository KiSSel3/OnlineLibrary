using System.Security.Claims;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.Services.Interfaces;
using OnlineLibrary.BLL.UseCases.Implementation.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.User;

public class RegisterUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly RegisterUseCase _useCase;

    public RegisterUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _tokenServiceMock = new Mock<ITokenService>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _configurationMock = new Mock<IConfiguration>();
        _mapperMock = new Mock<IMapper>();
        _useCase = new RegisterUseCase(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordServiceMock.Object, _configurationMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowAuthenticationException_WhenUserAlreadyExists()
    {
        // Arrange
        var registerRequest = new RegisterRequestDTO { Login = "existinguser", Password = "password" };
        var existingUser = new UserEntity();

        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IUserRepository>().GetByLoginAsync(registerRequest.Login, It.IsAny<CancellationToken>())).ReturnsAsync(existingUser);

        // Act & Assert
        await Assert.ThrowsAsync<AuthenticationException>(() => _useCase.ExecuteAsync(registerRequest, CancellationToken.None));
    }
}