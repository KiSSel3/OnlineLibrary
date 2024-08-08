using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Interfaces;

public interface IBookRepository : IBaseRepository<BookEntity>
{
    Task<IQueryable<BookEntity>> GetByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken = default);
    Task<IQueryable<BookEntity>> GetByGenreIdAsync(Guid genreId, CancellationToken cancellationToken = default);
}