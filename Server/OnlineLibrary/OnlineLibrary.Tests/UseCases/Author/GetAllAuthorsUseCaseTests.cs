using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.UseCases.Implementation.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;
using Xunit;

namespace OnlineLibrary.Tests.UseCases.Author;

public class GetAllAuthorsUseCaseTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetAllAuthorsUseCase _useCase;

    public GetAllAuthorsUseCaseTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _useCase = new GetAllAuthorsUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnAllAuthors()
    {
        // Arrange
        var authors = new List<AuthorEntity>
        {
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1970, 1, 1),
                Country = "USA"
            },
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1980, 5, 15),
                Country = "UK"
            }
        };

        var authorDtos = new List<AuthorResponseDTO>
        {
            new AuthorResponseDTO
            {
                Id = authors[0].Id,
                AuthorDTO = new AuthorDTO()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1970, 1, 1),
                    Country = "USA"
                }
            },
            new AuthorResponseDTO
            {
                Id = authors[1].Id,
                AuthorDTO = new AuthorDTO()
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1980, 5, 15),
                    Country = "UK"
                }
            }
        };

        _mockUnitOfWork.Setup(u => u.GetBaseRepository<AuthorEntity>().GetAllAsync(CancellationToken.None)).ReturnsAsync(authors);
        _mockMapper.Setup(m => m.Map<IEnumerable<AuthorResponseDTO>>(authors)).Returns(authorDtos);

        // Act
        var result = await _useCase.ExecuteAsync(CancellationToken.None);

        // Assert
        Assert.Equal(authorDtos, result);
    }
}