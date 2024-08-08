using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Interfaces;

public interface IBookRepository : IBaseRepository<BookEntity>
{
    IQueryable<BookEntity> GetBooksQueryable();
}