using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    IBaseRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : BaseEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}