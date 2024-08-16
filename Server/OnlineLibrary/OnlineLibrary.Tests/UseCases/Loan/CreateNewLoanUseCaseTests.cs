using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Request.Loan;
using OnlineLibrary.BLL.UseCases.Implementation.Loan;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Loan;

public class CreateNewLoanUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILoanRepository> _loanRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateNewLoanUseCase _useCase;

    public CreateNewLoanUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loanRepositoryMock = new Mock<ILoanRepository>();
        _mapperMock = new Mock<IMapper>();
        _unitOfWorkMock.Setup(u => u.GetCustomRepository<ILoanRepository>()).Returns(_loanRepositoryMock.Object);
        _useCase = new CreateNewLoanUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowInvalidOperationException_WhenBookIsNotAvailable()
    {
        // Arrange
        var loanRequestDTO = new LoanCreateRequestDTO { BookId = Guid.NewGuid(), DayCount = 7 };
        _loanRepositoryMock.Setup(r => r.IsBookAvailableAsync(loanRequestDTO.BookId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _useCase.ExecuteAsync(loanRequestDTO, CancellationToken.None));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateLoanAndSave_WhenBookIsAvailable()
    {
        // Arrange
        var loanRequestDTO = new LoanCreateRequestDTO { BookId = Guid.NewGuid(), DayCount = 7 };
        var loanEntity = new LoanEntity();

        _loanRepositoryMock.Setup(r => r.IsBookAvailableAsync(loanRequestDTO.BookId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mapperMock.Setup(m => m.Map<LoanEntity>(loanRequestDTO)).Returns(loanEntity);

        // Act
        await _useCase.ExecuteAsync(loanRequestDTO, CancellationToken.None);

        // Assert
        _loanRepositoryMock.Verify(r => r.CreateAsync(loanEntity, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}