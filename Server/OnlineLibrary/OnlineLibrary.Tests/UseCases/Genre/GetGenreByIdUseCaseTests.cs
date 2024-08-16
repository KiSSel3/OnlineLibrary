using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Genre;

public class GetGenreByIdUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetGenreByIdUseCase _useCase;

    public GetGenreByIdUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _useCase = new GetGenreByIdUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnMappedGenre_WhenGenreExists()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        var genreEntity = new GenreEntity { Id = genreId, Name = "Fantasy" };
        var genreDTO = new GenreDTO { Id = genreId, Name = "Fantasy" };

        _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>().GetByIdAsync(genreId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(genreEntity);
        _mapperMock.Setup(m => m.Map<GenreDTO>(It.IsAny<GenreEntity>()))
            .Returns(genreDTO);

        // Act
        var result = await _useCase.ExecuteAsync(genreId);

        // Assert
        Assert.Equal(genreDTO, result);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenGenreNotFound()
    {
        // Arrange
        var genreId = Guid.NewGuid();

        _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>().GetByIdAsync(genreId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((GenreEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(genreId));
    }
}