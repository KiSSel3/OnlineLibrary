using Microsoft.EntityFrameworkCore;
using OnlineLibrary.DAL.Infrastructure;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Repositories.Implementations;

public class LoanRepository : ILoanRepository
{
    private readonly AppDbContext _dbContext;

    public LoanRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(LoanEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Loans.AddAsync(entity, cancellationToken);
    }

    public async Task<LoanEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Loans
            .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<LoanEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Loans
            .Where(l => !l.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public void Delete(LoanEntity entity)
    {
        entity.IsDeleted = true;
        _dbContext.Loans.Update(entity);
    }

    public void Update(LoanEntity entity)
    {
        _dbContext.Loans.Update(entity);
    }

    public async Task<LoanEntity> GetByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Loans
            .FirstOrDefaultAsync(l => l.BookId == bookId && !l.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<LoanEntity>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Loans
            .Where(l => l.UserId == userId && !l.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}