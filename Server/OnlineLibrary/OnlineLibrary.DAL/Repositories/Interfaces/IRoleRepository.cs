using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Interfaces;

public interface IRoleRepository : IBaseRepository<RoleEntity>
{
    Task<RoleEntity> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<RoleEntity>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> CheckUserHasRoleAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task RemoveRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
    Task SetRoleToUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
}