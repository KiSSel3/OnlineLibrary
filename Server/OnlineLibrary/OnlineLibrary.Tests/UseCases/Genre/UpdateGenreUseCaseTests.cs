using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Genre;

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
        var genreDto = new GenreDTO { Id = Guid.NewGuid(), Name = "Updated Fantasy" };
        var genreEntity = new GenreEntity { Id = genreDto.Id, Name = "Old Fantasy" };

        var genreRepositoryMock = new Mock<IBaseRepository<GenreEntity>>();
        _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>()).Returns(genreRepositoryMock.Object);

        genreRepositoryMock.Setup(r => r.GetByIdAsync(genreDto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(genreEntity);

        _mapperMock.Setup(m => m.Map<GenreEntity>(genreDto))
            .Returns(new GenreEntity { Id = genreDto.Id, Name = genreDto.Name });

        // Act
        await _useCase.ExecuteAsync(genreDto);

        // Assert
        genreRepositoryMock.Verify(r => r.Update(It.Is<GenreEntity>(g => g.Id == genreDto.Id && g.Name == genreDto.Name)), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenGenreNotFound()
    {
        // Arrange
        var genreDto = new GenreDTO { Id = Guid.NewGuid() };

        var genreRepositoryMock = new Mock<IBaseRepository<GenreEntity>>();
        _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>()).Returns(genreRepositoryMock.Object);
        
        genreRepositoryMock.Setup(r => r.GetByIdAsync(genreDto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((GenreEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(genreDto));
    }
}