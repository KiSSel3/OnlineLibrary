using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.BLL.UseCases.Implementation.Loan;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Loan;

public class GetLoansByUserIdUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILoanRepository> _loanRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetLoansByUserIdUseCase _useCase;

    public GetLoansByUserIdUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loanRepositoryMock = new Mock<ILoanRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _bookRepositoryMock = new Mock<IBookRepository>();
        _mapperMock = new Mock<IMapper>();

        _unitOfWorkMock.Setup(u => u.GetCustomRepository<ILoanRepository>()).Returns(_loanRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IUserRepository>()).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.GetCustomRepository<IBookRepository>()).Returns(_bookRepositoryMock.Object);

        _useCase = new GetLoansByUserIdUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnLoanResponseDTOList()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var loanEntity = new LoanEntity { UserId = userId, BookId = Guid.NewGuid(), BorrowedAt = DateTime.UtcNow, ReturnBy = DateTime.UtcNow.AddDays(7) };
        var loans = new List<LoanEntity> { loanEntity };

        var userDTO = new UserResponseDTO();
        var bookDTO = new BookDTO();

        _loanRepositoryMock.Setup(r => r.GetByUserIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(loans);
        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(new UserEntity());
        _bookRepositoryMock.Setup(r => r.GetByIdAsync(loanEntity.BookId, It.IsAny<CancellationToken>())).ReturnsAsync(new BookEntity());

        _mapperMock.Setup(m => m.Map<UserResponseDTO>(It.IsAny<UserEntity>())).Returns(userDTO);
        _mapperMock.Setup(m => m.Map<BookDTO>(It.IsAny<BookEntity>())).Returns(bookDTO);

        // Act
        var result = await _useCase.ExecuteAsync(userId, CancellationToken.None);

        // Assert
        Assert.Single(result);
        var loanResponse = result.First();
        Assert.Equal(userDTO, loanResponse.UserDTO);
        Assert.Equal(bookDTO, loanResponse.BookDTO);
    }
}