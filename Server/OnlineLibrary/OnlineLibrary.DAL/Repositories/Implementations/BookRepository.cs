using Microsoft.EntityFrameworkCore;
using OnlineLibrary.DAL.Infrastructure;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Implementations;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _dbContext;

    public BookRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(BookEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Books.AddAsync(entity, cancellationToken);
    }

    public async Task<BookEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<BookEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books
            .Where(l => !l.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public void Delete(BookEntity entity)
    {
        entity.IsDeleted = true;
        _dbContext.Books.Update(entity);
    }

    public void Update(BookEntity entity)
    {
        _dbContext.Books.Update(entity);
    }

    public async Task<BookEntity> GetByISBNAsync(string isbn, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books
            .FirstOrDefaultAsync(b => b.ISBN == isbn && !b.IsDeleted, cancellationToken);
    }

    public IQueryable<BookEntity> GetBooksQueryable()
    {
        return _dbContext.Books
            .Where(b=>!b.IsDeleted)
            .AsQueryable();
    }
}