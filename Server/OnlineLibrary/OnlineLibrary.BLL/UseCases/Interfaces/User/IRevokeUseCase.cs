namespace OnlineLibrary.BLL.UseCases.Interfaces.User;

public interface IRevokeUseCase
{
    Task ExecuteAsync(Guid userId, CancellationToken cancellationToken = default);
}