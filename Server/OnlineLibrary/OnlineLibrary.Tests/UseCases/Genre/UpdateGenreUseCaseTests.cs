using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;
using Xunit;

namespace OnlineLibrary.Tests.UseCases.Genre
{
    public class UpdateGenreUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateGenreUseCase _useCase;

        public UpdateGenreUseCaseTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _useCase = new UpdateGenreUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldUpdateAndSaveGenre_WhenGenreExists()
        {
            // Arrange
            var genreId = Guid.NewGuid();
            var genreName = "Updated Fantasy";

            var existingGenre = new GenreEntity { Id = genreId, Name = "Old Fantasy" };

            var genreRepositoryMock = new Mock<IBaseRepository<GenreEntity>>();
            _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>()).Returns(genreRepositoryMock.Object);

            genreRepositoryMock.Setup(r => r.GetByIdAsync(genreId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingGenre);

            // Act
            await _useCase.ExecuteAsync(genreId, genreName);

            // Assert
            genreRepositoryMock.Verify(r => r.Update(It.Is<GenreEntity>(g => g.Id == genreId && g.Name == genreName)), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenGenreNotFound()
        {
            // Arrange
            var genreId = Guid.NewGuid();
            var genreName = "NonExisting Genre";

            var genreRepositoryMock = new Mock<IBaseRepository<GenreEntity>>();
            _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>()).Returns(genreRepositoryMock.Object);

            genreRepositoryMock.Setup(r => r.GetByIdAsync(genreId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((GenreEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(genreId, genreName));
        }
    }
}
