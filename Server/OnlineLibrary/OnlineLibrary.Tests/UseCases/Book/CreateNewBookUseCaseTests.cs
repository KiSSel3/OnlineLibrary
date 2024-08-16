using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.DTOs.Responses.Book;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Book;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;
using Xunit;

namespace OnlineLibrary.Tests.UseCases.Book;

public class CreateNewBookUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateNewBookUseCase _createNewBookUseCase;

    public CreateNewBookUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _createNewBookUseCase = new CreateNewBookUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateNewBook_WhenBookRequestDTOIsValid()
    {
        // Arrange
        var bookCreateRequestDTO = new BookCreateRequestDTO
        {
            BookDTO = new BookDTO { ISBN = "12345", Title = "Test Book", Description = "Test Description" },
            GenreId = Guid.NewGuid(),
            AuthorId = Guid.NewGuid()
        };
        var bookEntity = new BookEntity();
        _mapperMock.Setup(m => m.Map<BookEntity>(bookCreateRequestDTO)).Returns(bookEntity);

        var bookRepositoryMock = new Mock<IBookRepository>();
        _unitOfWorkMock.Setup(uow => uow.GetCustomRepository<IBookRepository>()).Returns(bookRepositoryMock.Object);

        // Act
        await _createNewBookUseCase.ExecuteAsync(bookCreateRequestDTO);

        // Assert
        bookRepositoryMock.Verify(repo => repo.CreateAsync(bookEntity, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}