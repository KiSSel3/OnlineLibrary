using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using Moq;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Author;
using OnlineLibrary.BLL.UseCases.Implementation.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;
using Xunit;

namespace OnlineLibrary.Tests.UseCases.Author;

public class CreateNewAuthorUseCaseTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateNewAuthorUseCase _useCase;

    public CreateNewAuthorUseCaseTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _useCase = new CreateNewAuthorUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateAuthor()
    {
        // Arrange
        var authorDto = new AuthorDTO {  };
        var authorEntity = new AuthorEntity {  };
        
        _mockMapper.Setup(m => m.Map<AuthorEntity>(authorDto)).Returns(authorEntity);
        var mockRepo = new Mock<IBaseRepository<AuthorEntity>>();
        _mockUnitOfWork.Setup(u => u.GetBaseRepository<AuthorEntity>()).Returns(mockRepo.Object);

        // Act
        await _useCase.ExecuteAsync(authorDto, CancellationToken.None);

        // Assert
        mockRepo.Verify(r => r.CreateAsync(authorEntity, CancellationToken.None), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}