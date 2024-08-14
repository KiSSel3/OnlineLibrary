using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Interfaces;

public interface ILoanRepository : IBaseRepository<LoanEntity>
{
    Task<LoanEntity> GetByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default);
    Task<IEnumerable<LoanEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> IsBookAvailableAsync(Guid bookId, CancellationToken cancellationToken = default);
}