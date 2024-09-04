using OnlineLibrary.Domain.Models;

namespace OnlineLibrary.BLL.Services.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(NotificationEventArgs e, CancellationToken cancellationToken = default);
}