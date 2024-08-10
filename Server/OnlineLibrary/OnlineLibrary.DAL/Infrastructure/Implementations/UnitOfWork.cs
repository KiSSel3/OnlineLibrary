using Microsoft.EntityFrameworkCore.Infrastructure;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Implementations;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private Dictionary<Type, object> _repositories;
    
    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, object>();
    }

    public IBaseRepository<TEntity> GetBaseRepository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity);
        if (!_repositories.TryGetValue(type, out object? baseRepository))
        {
            baseRepository = new BaseRepository<TEntity>(_dbContext);
            _repositories[type] = baseRepository;
        }

        return (IBaseRepository<TEntity>)baseRepository;
    }
    
    public TRepository GetCustomRepository<TRepository>() where TRepository : class
    {
        var type = typeof(TRepository);
        if (!_repositories.TryGetValue(type, out var customRepository))
        {
            customRepository = _dbContext.GetService<TRepository>();
            if (customRepository == null)
            {
                throw new NotImplementedException($"No repository found for type {type.FullName}");
            }
            _repositories[type] = customRepository;
        }

        return (TRepository)customRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}