namespace OnlineLibrary.BLL.UseCases.Interfaces.Loan;

public interface ICheckIsBookAvailableUseCase
{
    Task<bool> ExecuteAsync(Guid bookId, CancellationToken cancellationToken);
}