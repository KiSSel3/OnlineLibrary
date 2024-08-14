using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Models;

namespace OnlineLibrary.BLL.Infrastructure.BackgroundServices;

public class LoanNotificationService : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    
    public LoanNotificationService(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var intervalHours = _configuration.GetValue<int>("NotificationSettings:IntervalHours");
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                await CheckAndSendNotificationsAsync(unitOfWork, stoppingToken);
            }
            
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
            
            var notificationServices = _serviceProvider.GetServices<INotificationService>();
            
            foreach (var notificationService in notificationServices)
            {
                await notificationService.SendNotificationAsync(new NotificationEventArgs(){ Book = book, User = user, Loan = loan }, cancellationToken);
            }
        }
    }
}