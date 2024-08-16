namespace OnlineLibrary.BLL.UseCases.Interfaces.Loan;

public interface IDeleteLoanByBookIdUseCase
{
    Task ExecuteAsync(Guid bookId, CancellationToken cancellationToken);
}