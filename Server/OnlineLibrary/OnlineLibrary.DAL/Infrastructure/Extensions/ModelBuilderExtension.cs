using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Extensions;

public static class ModelBuilderExtension
{
    public static void SeedUsersRolesData(this ModelBuilder modelBuilder)
    {
        var adminRoleId = new Guid("F8F16246-CE18-4F8F-9657-AB5C95DD0FC4");
        modelBuilder.Entity<RoleEntity>().HasData(new RoleEntity
        {
            Id = adminRoleId,
            Name = "Admin"
        });

        var adminUserId = new Guid("6DC74677-D3B2-4DB9-B6C7-FF09B8720CD7");
        modelBuilder.Entity<UserEntity>().HasData(new UserEntity
        {
            Id = adminUserId,
            Login = "Admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin"),
            RefreshToken = "",
            RefreshTokenExpiryTime = DateTime.MinValue
        });

        modelBuilder.Entity<UserRoleEntity>().HasData(new UserRoleEntity
        {
            Id = Guid.NewGuid(),
            RoleId = adminRoleId,
            UserId = adminUserId
        });
        
        modelBuilder.Entity<RoleEntity>().HasData(new RoleEntity
        {
            Id = Guid.NewGuid(),
            Name = "User"
        });
    }
}