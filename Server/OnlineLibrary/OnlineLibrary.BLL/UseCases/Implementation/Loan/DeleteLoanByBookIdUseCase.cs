using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Loan;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.Loan;

public class DeleteLoanByBookIdUseCase : IDeleteLoanByBookIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLoanByBookIdUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var loanRepository = _unitOfWork.GetCustomRepository<ILoanRepository>();
        
        var loan = await loanRepository.GetByBookIdAsync(bookId, cancellationToken);
        if (loan == null)
        {
            throw new EntityNotFoundException($"Loan for book with ID {bookId} not found.");
        }

        loanRepository.Delete(loan);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}