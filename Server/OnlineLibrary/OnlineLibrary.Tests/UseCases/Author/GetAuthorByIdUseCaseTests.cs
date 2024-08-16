using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Tests.UseCases.Author;

public class GetAuthorByIdUseCaseTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetAuthorByIdUseCase _useCase;

    public GetAuthorByIdUseCaseTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _useCase = new GetAuthorByIdUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnAuthorById()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var author = new AuthorEntity { Id = authorId };
        var authorDto = new AuthorResponseDTO {  };

        _mockUnitOfWork.Setup(u => u.GetBaseRepository<AuthorEntity>().GetByIdAsync(authorId, CancellationToken.None)).ReturnsAsync(author);
        _mockMapper.Setup(m => m.Map<AuthorResponseDTO>(author)).Returns(authorDto);

        // Act
        var result = await _useCase.ExecuteAsync(authorId, CancellationToken.None);

        // Assert
        Assert.Equal(authorDto, result);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenAuthorNotFound()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        _mockUnitOfWork.Setup(u => u.GetBaseRepository<AuthorEntity>().GetByIdAsync(authorId, CancellationToken.None)).ReturnsAsync((AuthorEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(authorId, CancellationToken.None));
    }
}