namespace OnlineLibrary.Tests.UseCases.Loan;

using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using OnlineLibrary.BLL.UseCases.Implementation.Loan;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

public class CheckIsBookAvailableUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILoanRepository> _loanRepositoryMock;
    private readonly CheckIsBookAvailableUseCase _useCase;

    public CheckIsBookAvailableUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loanRepositoryMock = new Mock<ILoanRepository>();
        _unitOfWorkMock.Setup(u => u.GetCustomRepository<ILoanRepository>()).Returns(_loanRepositoryMock.Object);
        _useCase = new CheckIsBookAvailableUseCase(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnTrue_WhenBookIsAvailable()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _loanRepositoryMock.Setup(r => r.IsBookAvailableAsync(bookId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _useCase.ExecuteAsync(bookId, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFalse_WhenBookIsNotAvailable()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _loanRepositoryMock.Setup(r => r.IsBookAvailableAsync(bookId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _useCase.ExecuteAsync(bookId, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}
