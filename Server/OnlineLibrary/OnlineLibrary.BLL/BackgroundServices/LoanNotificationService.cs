using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineLibrary.BLL.Services.Interfaces;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Models;

namespace OnlineLibrary.BLL.BackgroundServices;

public class LoanNotificationService : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<LoanNotificationService> _logger;
    
    public LoanNotificationService(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<LoanNotificationService> logger)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    await CheckAndSendNotificationsAsync(unitOfWork, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"LoanNotificationService error: {ex.Message}");
            }
            
            var intervalHours = _configuration.GetValue<int>("NotificationSettings:IntervalHours");
            await Task.Delay(TimeSpan.FromHours(intervalHours), stoppingToken);
        }
    }

    private async Task CheckAndSendNotificationsAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        var noticePeriodDay = _configuration.GetValue<int>("NotificationSettings:NoticePeriodDay");
        
        var soonToBeReturnedLoans = await unitOfWork.GetCustomRepository<ILoanRepository>()
            .GetLoansDueForReturnAsync(DateTime.UtcNow.AddDays(noticePeriodDay), cancellationToken);

        foreach (var loan in soonToBeReturnedLoans)
        {
            var user = await unitOfWork.GetBaseRepository<UserEntity>().GetByIdAsync(loan.UserId, cancellationToken);
            var book = await unitOfWork.GetBaseRepository<BookEntity>().GetByIdAsync(loan.BookId, cancellationToken);

            if (user == null || book == null)
            {
                continue;
            }
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var notificationServices = scope.ServiceProvider.GetServices<INotificationService>();

                foreach (var notificationService in notificationServices)
                {
                    try
                    {
                        await notificationService.SendNotificationAsync(new NotificationEventArgs
                        {
                            Book = book,
                            User = user,
                            Loan = loan
                        }, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending notification.");
                    }
                }
            }
        }
    }
}