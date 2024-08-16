using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Interfaces;

public interface IBookRepository : IBaseRepository<BookEntity>
{
    Task<BookEntity> GetByISBNAsync(string isbn, CancellationToken cancellationToken = default);
    IQueryable<BookEntity> GetBooksQueryable();
}