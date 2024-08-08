using Microsoft.EntityFrameworkCore;
using OnlineLibrary.DAL.Infrastructure;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Implementations;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _dbContext;

    public RoleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(RoleEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.AddAsync(entity, cancellationToken);
    }

    public async Task<RoleEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<RoleEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .Where(r => !r.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public void Delete(RoleEntity entity)
    {
        entity.IsDeleted = true;
        _dbContext.Roles.Update(entity);
    }

    public void Update(RoleEntity entity)
    {
        _dbContext.Roles.Update(entity);
    }

    public async Task<RoleEntity> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .FirstOrDefaultAsync(r => r.Name == name && !r.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<RoleEntity>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        //TODO: It may be necessary to modify it to use navigation properties
        return await _dbContext.UsersRoles
            .Where(ur => ur.UserId == userId && !ur.IsDeleted)
            .Join(_dbContext.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => r)
            .Where(r => !r.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> CheckUserHasRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UsersRoles
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId && !ur.IsDeleted, cancellationToken);
    }

    public async Task SetRoleToUserAsync(UserRoleEntity userRole, CancellationToken cancellationToken = default)
    {
        await _dbContext.UsersRoles.AddAsync(userRole, cancellationToken);
    }
    
    public async Task RemoveRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        var userRole = await _dbContext.UsersRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId && !ur.IsDeleted, cancellationToken);

        if (userRole != null)
        {
            userRole.IsDeleted = true;
            _dbContext.UsersRoles.Update(userRole);
        }
    }
}