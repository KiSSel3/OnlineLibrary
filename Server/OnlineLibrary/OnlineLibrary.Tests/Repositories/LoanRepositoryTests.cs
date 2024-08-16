using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.DAL.Infrastructure;
using OnlineLibrary.DAL.Repositories.Implementations;
using OnlineLibrary.Domain.Entities;
using Xunit;

namespace OnlineLibrary.Tests.Repositories;

public class LoanRepositoryTests
{
    private AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Loan_To_Database()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new LoanRepository(context);

        var loan = new LoanEntity
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BorrowedAt = DateTime.UtcNow,
            ReturnBy = DateTime.UtcNow.AddDays(14)
        };

        // Act
        await repository.CreateAsync(loan);
        await context.SaveChangesAsync();

        // Assert
        var savedLoan = await context.Loans.FirstOrDefaultAsync(l => l.BookId == loan.BookId);
        Assert.NotNull(savedLoan);
        Assert.Equal(loan.UserId, savedLoan.UserId);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Correct_Loan()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new LoanRepository(context);

        var loan = new LoanEntity
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BorrowedAt = DateTime.UtcNow,
            ReturnBy = DateTime.UtcNow.AddDays(14)
        };

        await context.Loans.AddAsync(loan);
        await context.SaveChangesAsync();

        // Act
        var retrievedLoan = await repository.GetByIdAsync(loan.Id);

        // Assert
        Assert.NotNull(retrievedLoan);
        Assert.Equal(loan.Id, retrievedLoan.Id);
    }

    [Fact]
    public async Task Delete_Should_Mark_Loan_As_Deleted()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new LoanRepository(context);

        var loan = new LoanEntity
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BorrowedAt = DateTime.UtcNow,
            ReturnBy = DateTime.UtcNow.AddDays(14)
        };

        await context.Loans.AddAsync(loan);
        await context.SaveChangesAsync();

        // Act
        repository.Delete(loan);
        await context.SaveChangesAsync();

        // Assert
        var deletedLoan = await context.Loans.FirstOrDefaultAsync(l => l.Id == loan.Id);
        Assert.NotNull(deletedLoan);
        Assert.True(deletedLoan.IsDeleted);
    }

    [Fact]
    public async Task IsBookAvailableAsync_Should_Return_True_If_Book_Is_Available()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new LoanRepository(context);

        var bookId = Guid.NewGuid();

        var loan = new LoanEntity
        {
            BookId = bookId,
            UserId = Guid.NewGuid(),
            BorrowedAt = DateTime.UtcNow,
            ReturnBy = DateTime.UtcNow.AddDays(14),
            IsDeleted = true
        };

        await context.Loans.AddAsync(loan);
        await context.SaveChangesAsync();

        // Act
        var isAvailable = await repository.IsBookAvailableAsync(bookId);

        // Assert
        Assert.True(isAvailable);
    }

    [Fact]
    public async Task GetLoansDueForReturnAsync_Should_Return_Loans_With_Due_Dates()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new LoanRepository(context);

        var loan1 = new LoanEntity
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BorrowedAt = DateTime.UtcNow.AddDays(-10),
            ReturnBy = DateTime.UtcNow.AddDays(-5)
        };

        var loan2 = new LoanEntity
        {
            BookId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BorrowedAt = DateTime.UtcNow.AddDays(-20),
            ReturnBy = DateTime.UtcNow.AddDays(-2)
        };

        await context.Loans.AddRangeAsync(loan1, loan2);
        await context.SaveChangesAsync();

        // Act
        var dueLoans = await repository.GetLoansDueForReturnAsync(DateTime.UtcNow);

        // Assert
        Assert.Contains(dueLoans, l => l.Id == loan1.Id);
        Assert.Contains(dueLoans, l => l.Id == loan2.Id);
    }
}