using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
}