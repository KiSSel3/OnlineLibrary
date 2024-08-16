using Moq;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Loan;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Loan;

public class DeleteLoanByBookIdUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILoanRepository> _loanRepositoryMock;
    private readonly DeleteLoanByBookIdUseCase _useCase;

    public DeleteLoanByBookIdUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loanRepositoryMock = new Mock<ILoanRepository>();
        _unitOfWorkMock.Setup(u => u.GetCustomRepository<ILoanRepository>()).Returns(_loanRepositoryMock.Object);
        _useCase = new DeleteLoanByBookIdUseCase(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenLoanNotFound()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _loanRepositoryMock.Setup(r => r.GetByBookIdAsync(bookId, It.IsAny<CancellationToken>())).ReturnsAsync((LoanEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(bookId, CancellationToken.None));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldDeleteLoanAndSave_WhenLoanExists()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var loanEntity = new LoanEntity();

        _loanRepositoryMock.Setup(r => r.GetByBookIdAsync(bookId, It.IsAny<CancellationToken>())).ReturnsAsync(loanEntity);

        // Act
        await _useCase.ExecuteAsync(bookId, CancellationToken.None);

        // Assert
        _loanRepositoryMock.Verify(r => r.Delete(loanEntity), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}