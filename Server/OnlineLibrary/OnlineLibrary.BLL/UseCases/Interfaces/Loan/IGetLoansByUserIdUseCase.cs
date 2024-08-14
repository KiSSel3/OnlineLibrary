using OnlineLibrary.BLL.DTOs.Responses.Loan;

namespace OnlineLibrary.BLL.UseCases.Interfaces.Loan;

public interface IGetLoansByUserIdUseCase
{
    Task<IEnumerable<LoanResponseDTO>> ExecuteAsync(Guid userId, CancellationToken cancellationToken);
}