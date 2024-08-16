using System.Security.Claims;
using Moq;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.BLL.UseCases.Implementation.User;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace OnlineLibrary.Tests.UseCases.User;

public class LoginUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly LoginUseCase _useCase;

    public LoginUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _tokenServiceMock = new Mock<ITokenService>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _configurationMock = new Mock<IConfiguration>();
        _useCase = new LoginUseCase(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordServiceMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowAuthenticationException_WhenPasswordIsIncorrect()
    {
        // Arrange
        var loginRequest = new LoginRequestDTO { Login = "testuser", Password = "wrongpassword" };
        var user = new UserEntity { PasswordHash = "hashedpassword" };

        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IUserRepository>().GetByLoginAsync(loginRequest.Login, It.IsAny<CancellationToken>())).ReturnsAsync(user);
        _passwordServiceMock.Setup(p => p.VerifyPassword(user.PasswordHash, loginRequest.Password)).Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<AuthenticationException>(() => _useCase.ExecuteAsync(loginRequest, CancellationToken.None));
    }
}