using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.DAL.Infrastructure;
using OnlineLibrary.DAL.Repositories.Implementations;
using OnlineLibrary.Domain.Entities;
using Xunit;

namespace OnlineLibrary.Tests.Repositories;

public class BookRepositoryTests
{
    private readonly AppDbContext _dbContext;
    private readonly BookRepository _bookRepository;

    public BookRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dbContext = new AppDbContext(options);
        _bookRepository = new BookRepository(_dbContext);
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Book()
    {
        // Arrange
        var book = new BookEntity
        {
            Id = Guid.NewGuid(),
            ISBN = "123456789",
            Title = "Test Book",
            Description = "Test Description"
        };

        // Act
        await _bookRepository.CreateAsync(book);
        await _dbContext.SaveChangesAsync();

        // Assert
        var createdBook = await _dbContext.Books.FindAsync(book.Id);
        Assert.NotNull(createdBook);
        Assert.Equal("Test Book", createdBook.Title);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Correct_Book()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var book = new BookEntity
        {
            Id = bookId,
            ISBN = "123456789",
            Title = "Test Book",
            Description = "Test Description"
        };
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrievedBook = await _bookRepository.GetByIdAsync(bookId);

        // Assert
        Assert.NotNull(retrievedBook);
        Assert.Equal(bookId, retrievedBook.Id);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Books()
    {
        // Arrange
        _dbContext.Books.AddRange(new[]
        {
            new BookEntity { Id = Guid.NewGuid(), Title = "Test Book 1", ISBN = "111111", Description = "Test Description 1" },
            new BookEntity { Id = Guid.NewGuid(), Title = "Test Book 2", ISBN = "222222", Description = "Test Description 2"}
        });
        await _dbContext.SaveChangesAsync();

        // Act
        var books = await _bookRepository.GetAllAsync();

        // Assert
        Assert.Equal(2, books.Count());
    }

    [Fact]
    public async Task Delete_Should_Mark_Book_As_Deleted()
    {
        // Arrange
        var book = new BookEntity
        {
            Id = Guid.NewGuid(),
            Title = "Test Book",
            Description = "Test Description",
            ISBN = "123456789"
        };
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();

        // Act
        _bookRepository.Delete(book);
        await _dbContext.SaveChangesAsync();

        // Assert
        var deletedBook = await _dbContext.Books.FindAsync(book.Id);
        Assert.True(deletedBook.IsDeleted);
    }

    [Fact]
    public async Task Update_Should_Modify_Book()
    {
        // Arrange
        var book = new BookEntity
        {
            Id = Guid.NewGuid(),
            Title = "Test Book",
            ISBN = "123456789",
            Description = "Test Description"
        };
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();

        // Act
        book.Title = "Updated Book";
        _bookRepository.Update(book);
        await _dbContext.SaveChangesAsync();

        // Assert
        var updatedBook = await _dbContext.Books.FindAsync(book.Id);
        Assert.Equal("Updated Book", updatedBook.Title);
    }

    [Fact]
    public async Task GetByISBNAsync_Should_Return_Correct_Book()
    {
        // Arrange
        var book = new BookEntity
        {
            Id = Guid.NewGuid(),
            Title = "Test Book",
            ISBN = "123456789",
            Description = "Test Description"
        };
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrievedBook = await _bookRepository.GetByISBNAsync("123456789");

        // Assert
        Assert.NotNull(retrievedBook);
        Assert.Equal("123456789", retrievedBook.ISBN);
    }

    [Fact]
    public void GetBooksQueryable_Should_Return_Queryable_Collection()
    {
        // Arrange
        _dbContext.Books.AddRange(new[]
        {
            new BookEntity { Id = Guid.NewGuid(), Title = "Test Book 1", ISBN = "111111", Description = "Test Description 1" },
            new BookEntity { Id = Guid.NewGuid(), Title = "Test Book 2", ISBN = "222222", Description = "Test Description 2" }
        });
        _dbContext.SaveChanges();

        // Act
        var booksQueryable = _bookRepository.GetBooksQueryable();

        // Assert
        Assert.Equal(2, booksQueryable.Count());
    }
}