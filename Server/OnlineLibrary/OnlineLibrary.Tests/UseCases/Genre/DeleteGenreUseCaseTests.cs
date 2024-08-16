using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using OnlineLibrary.BLL.UseCases.Implementation.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.Tests.UseCases.Genre;

public class DeleteGenreUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteGenreUseCase _useCase;

    public DeleteGenreUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _useCase = new DeleteGenreUseCase(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldDeleteGenre_WhenGenreExists()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        var genreEntity = new GenreEntity { Id = genreId };

        var genreRepositoryMock = new Mock<IBaseRepository<GenreEntity>>();
        genreRepositoryMock.Setup(r => r.GetByIdAsync(genreId, It.IsAny<CancellationToken>())).ReturnsAsync(genreEntity);
        _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>()).Returns(genreRepositoryMock.Object);

        // Act
        await _useCase.ExecuteAsync(genreId);

        // Assert
        genreRepositoryMock.Verify(r => r.Delete(It.Is<GenreEntity>(g => g.Id == genreId)), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenGenreNotFound()
    {
        // Arrange
        var genreId = Guid.NewGuid();

        var genreRepositoryMock = new Mock<IBaseRepository<GenreEntity>>();
        genreRepositoryMock.Setup(r => r.GetByIdAsync(genreId, It.IsAny<CancellationToken>())).ReturnsAsync((GenreEntity)null);
        _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>()).Returns(genreRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(genreId));
    }
}
