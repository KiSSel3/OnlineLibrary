using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.DAL.Infrastructure;
using OnlineLibrary.DAL.Repositories.Implementations;
using OnlineLibrary.Domain.Entities;
using Xunit;

namespace OnlineLibrary.Tests.Repositories;

public class UserRepositoryTests
{
    private AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_Should_Add_User_To_Database()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new UserRepository(context);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Login = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "hashedpassword",
            RefreshToken = "refreshtoken",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
        };

        // Act
        await repository.CreateAsync(user);
        await context.SaveChangesAsync();

        // Assert
        var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
        Assert.NotNull(savedUser);
        Assert.Equal(user.Login, savedUser.Login);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Correct_User()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new UserRepository(context);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Login = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "hashedpassword",
            RefreshToken = "refreshtoken",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Act
        var retrievedUser = await repository.GetByIdAsync(user.Id);

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Equal(user.Id, retrievedUser.Id);
    }

    [Fact]
    public async Task Delete_Should_Mark_User_As_Deleted()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new UserRepository(context);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Login = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "hashedpassword",
            RefreshToken = "refreshtoken",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Act
        repository.Delete(user);
        await context.SaveChangesAsync();

        // Assert
        var deletedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        Assert.NotNull(deletedUser);
        Assert.True(deletedUser.IsDeleted);
    }

    [Fact]
    public async Task Update_Should_Modify_User_Details()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new UserRepository(context);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Login = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "hashedpassword",
            RefreshToken = "refreshtoken",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Act
        user.Email = "updatedemail@example.com";
        repository.Update(user);
        await context.SaveChangesAsync();

        // Assert
        var updatedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        Assert.NotNull(updatedUser);
        Assert.Equal("updatedemail@example.com", updatedUser.Email);
    }

    [Fact]
    public async Task GetByLoginAsync_Should_Return_User_By_Login()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new UserRepository(context);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Login = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "hashedpassword",
            RefreshToken = "refreshtoken",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Act
        var retrievedUser = await repository.GetByLoginAsync("testuser");

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Equal(user.Login, retrievedUser.Login);
    }

    [Fact]
    public async Task GetByRefreshTokenAsync_Should_Return_User_By_RefreshToken()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new UserRepository(context);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Login = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "hashedpassword",
            RefreshToken = "refreshtoken",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Act
        var retrievedUser = await repository.GetByRefreshTokenAsync("refreshtoken");

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Equal(user.RefreshToken, retrievedUser.RefreshToken);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Users()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        context.Users.RemoveRange(context.Users);
        var repository = new UserRepository(context);

        var user1 = new UserEntity
        {
            Id = Guid.NewGuid(),
            Login = "user1",
            Email = "user1@example.com",
            PasswordHash = "hashedpassword1",
            RefreshToken = "refreshtoken1",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
        };

        var user2 = new UserEntity
        {
            Id = Guid.NewGuid(),
            Login = "user2",
            Email = "user2@example.com",
            PasswordHash = "hashedpassword2",
            RefreshToken = "refreshtoken2",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1)
        };

        await context.Users.AddRangeAsync(user1, user2);
        await context.SaveChangesAsync();

        // Act
        var users = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(users);
        Assert.Equal(2, users.Count());
        Assert.Contains(users, u => u.Login == "user1");
        Assert.Contains(users, u => u.Login == "user2");
    }
}