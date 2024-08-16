using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Author;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Implementation.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;
using Xunit;

namespace OnlineLibrary.Tests.UseCases.Author;

public class UpdateAuthorUseCaseTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UpdateAuthorUseCase _useCase;

    public UpdateAuthorUseCaseTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _useCase = new UpdateAuthorUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldUpdateAuthor()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var updateRequest = new AuthorUpdateRequestDTO
        {
            Id = authorId,
            AuthorDTO = new AuthorDTO()
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                DateOfBirth = new DateTime(1990, 1, 1),
                Country = "UpdatedCountry"
            }
        };

        var existingAuthor = new AuthorEntity
        {
            Id = authorId,
            FirstName = "OriginalFirstName",
            LastName = "OriginalLastName",
            DateOfBirth = new DateTime(1980, 5, 15),
            Country = "OriginalCountry"
        };

        var updatedAuthor = new AuthorEntity
        {
            Id = authorId,
            FirstName = "UpdatedFirstName",
            LastName = "UpdatedLastName",
            DateOfBirth = new DateTime(1990, 1, 1),
            Country = "UpdatedCountry"
        };

        // Setup mocks
        var mockRepo = new Mock<IBaseRepository<AuthorEntity>>();
        mockRepo.Setup(r => r.GetByIdAsync(authorId, CancellationToken.None)).ReturnsAsync(existingAuthor);
        _mockUnitOfWork.Setup(u => u.GetBaseRepository<AuthorEntity>()).Returns(mockRepo.Object);
        _mockMapper.Setup(m => m.Map<AuthorEntity>(updateRequest)).Returns(updatedAuthor);

        // Act
        await _useCase.ExecuteAsync(updateRequest, CancellationToken.None);

        // Assert
        mockRepo.Verify(r => r.Update(updatedAuthor), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowEntityNotFoundException_WhenAuthorNotFound()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var updateRequest = new AuthorUpdateRequestDTO
        {
            Id = authorId,
            AuthorDTO = new AuthorDTO()
            {
                FirstName = "NonExistingFirstName",
                LastName = "NonExistingLastName",
                DateOfBirth = new DateTime(2000, 1, 1),
                Country = "NonExistingCountry"
            }
        };

        var mockRepo = new Mock<IBaseRepository<AuthorEntity>>();
        mockRepo.Setup(r => r.GetByIdAsync(authorId, CancellationToken.None)).ReturnsAsync((AuthorEntity)null);
        _mockUnitOfWork.Setup(u => u.GetBaseRepository<AuthorEntity>()).Returns(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _useCase.ExecuteAsync(updateRequest, CancellationToken.None));
    }
}
