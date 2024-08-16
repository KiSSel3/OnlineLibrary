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

public class RoleRepositoryTests
{
    private AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Role_To_Database()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new RoleRepository(context);

        var role = new RoleEntity
        {
            Id = Guid.NewGuid(),
            Name = "Admin"
        };

        // Act
        await repository.CreateAsync(role);
        await context.SaveChangesAsync();

        // Assert
        var savedRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == role.Name);
        Assert.NotNull(savedRole);
        Assert.Equal(role.Name, savedRole.Name);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Correct_Role()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new RoleRepository(context);

        var role = new RoleEntity
        {
            Id = Guid.NewGuid(),
            Name = "User"
        };

        await context.Roles.AddAsync(role);
        await context.SaveChangesAsync();

        // Act
        var retrievedRole = await repository.GetByIdAsync(role.Id);

        // Assert
        Assert.NotNull(retrievedRole);
        Assert.Equal(role.Id, retrievedRole.Id);
    }

    [Fact]
    public async Task Delete_Should_Mark_Role_As_Deleted()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new RoleRepository(context);

        var role = new RoleEntity
        {
            Id = Guid.NewGuid(),
            Name = "Moderator"
        };

        await context.Roles.AddAsync(role);
        await context.SaveChangesAsync();

        // Act
        repository.Delete(role);
        await context.SaveChangesAsync();

        // Assert
        var deletedRole = await context.Roles.FirstOrDefaultAsync(r => r.Id == role.Id);
        Assert.NotNull(deletedRole);
        Assert.True(deletedRole.IsDeleted);
    }

    [Fact]
    public async Task GetByNameAsync_Should_Return_Correct_Role()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new RoleRepository(context);

        var role = new RoleEntity
        {
            Id = Guid.NewGuid(),
            Name = "User"
        };

        await context.Roles.AddAsync(role);
        await context.SaveChangesAsync();

        // Act
        var retrievedRole = await repository.GetByNameAsync(role.Name);

        // Assert
        Assert.NotNull(retrievedRole);
        Assert.Equal(role.Name, retrievedRole.Name);
    }

    [Fact]
    public async Task SetRoleToUserAsync_Should_Assign_Role_To_User()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new RoleRepository(context);

        var userId = Guid.NewGuid();
        var roleId = Guid.NewGuid();

        var userRole = new UserRoleEntity
        {
            UserId = userId,
            RoleId = roleId
        };

        // Act
        await repository.SetRoleToUserAsync(userRole);
        await context.SaveChangesAsync();

        // Assert
        var savedUserRole = await context.UsersRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        Assert.NotNull(savedUserRole);
        Assert.Equal(userRole.UserId, savedUserRole.UserId);
        Assert.Equal(userRole.RoleId, savedUserRole.RoleId);
    }

    [Fact]
    public async Task RemoveRoleFromUserAsync_Should_Mark_UserRole_As_Deleted()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new RoleRepository(context);

        var userId = Guid.NewGuid();
        var roleId = Guid.NewGuid();

        var userRole = new UserRoleEntity
        {
            UserId = userId,
            RoleId = roleId
        };

        await context.UsersRoles.AddAsync(userRole);
        await context.SaveChangesAsync();

        // Act
        await repository.RemoveRoleFromUserAsync(userId, roleId);
        await context.SaveChangesAsync();

        // Assert
        var deletedUserRole = await context.UsersRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        Assert.NotNull(deletedUserRole);
        Assert.True(deletedUserRole.IsDeleted);
    }

    [Fact]
    public async Task CheckUserHasRoleAsync_Should_Return_True_If_User_Has_Role()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new RoleRepository(context);

        var userId = Guid.NewGuid();
        var roleId = Guid.NewGuid();

        var userRole = new UserRoleEntity
        {
            UserId = userId,
            RoleId = roleId
        };

        await context.UsersRoles.AddAsync(userRole);
        await context.SaveChangesAsync();

        // Act
        var hasRole = await repository.CheckUserHasRoleAsync(userId, roleId);

        // Assert
        Assert.True(hasRole);
    }

    [Fact]
    public async Task GetRolesByUserIdAsync_Should_Return_All_Roles_For_User()
    {
        // Arrange
        await using var context = CreateInMemoryDbContext();
        var repository = new RoleRepository(context);

        var userId = Guid.NewGuid();
        var role1 = new RoleEntity { Id = Guid.NewGuid(), Name = "Admin" };
        var role2 = new RoleEntity { Id = Guid.NewGuid(), Name = "User" };

        await context.Roles.AddRangeAsync(role1, role2);
        await context.UsersRoles.AddRangeAsync(
            new UserRoleEntity { UserId = userId, RoleId = role1.Id },
            new UserRoleEntity { UserId = userId, RoleId = role2.Id });
        await context.SaveChangesAsync();

        // Act
        var roles = await repository.GetRolesByUserIdAsync(userId);

        // Assert
        Assert.Contains(roles, r => r.Id == role1.Id);
        Assert.Contains(roles, r => r.Id == role2.Id);
    }
}