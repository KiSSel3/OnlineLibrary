using OnlineLibrary.BLL.DTOs.Request.Loan;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Loan;

public interface ICreateNewLoanUseCase
{
    Task ExecuteAsync(LoanCreateRequestDTO loanRequestDTO, CancellationToken cancellationToken);
}