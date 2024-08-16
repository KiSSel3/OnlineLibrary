using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task<UserEntity> GetByLoginAsync(string login, CancellationToken cancellationToken = default);
    Task<UserEntity> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}