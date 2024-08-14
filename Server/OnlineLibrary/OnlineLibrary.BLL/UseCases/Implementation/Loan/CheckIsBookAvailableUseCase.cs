using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Loan;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.Loan;

public class CheckIsBookAvailableUseCase : ICheckIsBookAvailableUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckIsBookAvailableUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid bookId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetCustomRepository<ILoanRepository>().IsBookAvailableAsync(bookId, cancellationToken);
    }
}