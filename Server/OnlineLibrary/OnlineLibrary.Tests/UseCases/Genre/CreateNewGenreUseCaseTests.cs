using OnlineLibrary.DAL.Repositories.Interfaces;
using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using OnlineLibrary.BLL.UseCases.Implementation.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Genre;

public class CreateNewGenreUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateNewGenreUseCase _useCase;

    public CreateNewGenreUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _useCase = new CreateNewGenreUseCase(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateAndSaveNewGenre()
    {
        // Arrange
        var genreName = "Fantasy";
        var genreEntity = new GenreEntity { Name = genreName };

        var genreRepositoryMock = new Mock<IBaseRepository<GenreEntity>>();
        _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>()).Returns(genreRepositoryMock.Object);

        // Act
        await _useCase.ExecuteAsync(genreName);

        // Assert
        genreRepositoryMock.Verify(r => r.CreateAsync(It.Is<GenreEntity>(g => g.Name == genreName), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
