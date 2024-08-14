using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.Domain.Models;

namespace OnlineLibrary.BLL.Infrastructure.Services.Implementation;

public class EmailNotificationService : INotificationService
{
    public Task SendNotificationAsync(NotificationEventArgs e, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}