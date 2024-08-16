using Moq;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Book;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Book;

public class DeleteBookUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteBookUseCase _deleteBookUseCase;

    public DeleteBookUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _deleteBookUseCase = new DeleteBookUseCase(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldDeleteBook_WhenBookExists()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var bookEntity = new BookEntity { Id = bookId };

        var bookRepositoryMock = new Mock<IBookRepository>();
        bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookEntity);
        _unitOfWorkMock.Setup(uow => uow.GetCustomRepository<IBookRepository>())
            .Returns(bookRepositoryMock.Object);

        // Act
        await _deleteBookUseCase.ExecuteAsync(bookId);

        // Assert
        bookRepositoryMock.Verify(repo => repo.Delete(bookEntity), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var bookRepositoryMock = new Mock<IBookRepository>();
        bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((BookEntity)null);
        _unitOfWorkMock.Setup(uow => uow.GetCustomRepository<IBookRepository>())
            .Returns(bookRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _deleteBookUseCase.ExecuteAsync(bookId));
    }
}