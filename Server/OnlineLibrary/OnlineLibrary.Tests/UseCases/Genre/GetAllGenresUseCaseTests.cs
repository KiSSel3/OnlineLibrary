using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using OnlineLibrary.BLL.UseCases.Implementation.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Genre;

namespace OnlineLibrary.Tests.UseCases.Genre;


public class GetAllGenresUseCaseTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllGenresUseCase _useCase;

    public GetAllGenresUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _useCase = new GetAllGenresUseCase(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnMappedGenres()
    {
        // Arrange
        var genres = new List<GenreEntity> { new GenreEntity { Name = "Fantasy" } };
        var genreDTOs = new List<GenreResponseDTO> { new GenreResponseDTO() { Name = "Fantasy" } };

        _unitOfWorkMock.Setup(u => u.GetBaseRepository<GenreEntity>().GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(genres);
        _mapperMock.Setup(m => m.Map<IEnumerable<GenreResponseDTO>>(It.IsAny<IEnumerable<GenreEntity>>()))
            .Returns(genreDTOs);

        // Act
        var result = await _useCase.ExecuteAsync();

        // Assert
        Assert.Equal(genreDTOs, result);
    }
}
