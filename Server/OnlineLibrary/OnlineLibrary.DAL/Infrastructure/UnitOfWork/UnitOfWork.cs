using Microsoft.EntityFrameworkCore.Infrastructure;
using OnlineLibrary.DAL.Repositories.Implementations;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

//TODO: Change namespace
namespace OnlineLibrary.DAL.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private Dictionary<Type, object> _repositories;
    
    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, object>();
    }

    public IBaseRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : BaseEntity
    {
        if (hasCustomRepository)
        {
            var customRepository = _dbContext.GetService<IBaseRepository<TEntity>>();
            if (customRepository != null)
            {
                return customRepository;
            }
        }

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new BaseRepository<TEntity>(_dbContext);
        }

        return (IBaseRepository<TEntity>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}