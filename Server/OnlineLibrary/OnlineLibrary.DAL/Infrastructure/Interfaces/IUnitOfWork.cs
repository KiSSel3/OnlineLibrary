using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    IBaseRepository<TEntity> GetBaseRepository<TEntity>() where TEntity : BaseEntity;
    TRepository GetCustomRepository<TRepository>() where TRepository : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}