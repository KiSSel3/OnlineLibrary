using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;
using Xunit;

namespace OnlineLibrary.Tests.UseCases.Author;

public class DeleteAuthorUseCaseTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly DeleteAuthorUseCase _useCase;

    public DeleteAuthorUseCaseTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _useCase = new DeleteAuthorUseCase(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldDeleteAuthor()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var author = new AuthorEntity { Id = authorId };

        var mockRepo = new Mock<IBaseRepository<AuthorEntity>>();
        mockRepo.Setup(r => r.GetByIdAsync(authorId, CancellationToken.None)).ReturnsAsync(author);
        _mockUnitOfWork.Setup(u => u.GetBaseRepository<AuthorEntity>()).Returns(mockRepo.Object);

        // Act
        await _useCase.ExecuteAsync(authorId, CancellationToken.None);

        // Assert
        mockRepo.Verify(r => r.Delete(author), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenAuthorNotFound()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var mockRepo = new Mock<IBaseRepository<AuthorEntity>>();
        mockRepo.Setup(r => r.GetByIdAsync(authorId, CancellationToken.None)).ReturnsAsync((AuthorEntity)null);
        _mockUnitOfWork.Setup(u => u.GetBaseRepository<AuthorEntity>()).Returns(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(authorId, CancellationToken.None));
    }
}