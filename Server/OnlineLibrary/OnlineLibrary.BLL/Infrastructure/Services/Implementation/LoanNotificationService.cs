using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Models;

namespace OnlineLibrary.BLL.Infrastructure.Services.Implementation;

public class LoanNotificationService : BackgroundService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    
    public delegate Task NotificationNeededAsyncHandler(NotificationEventArgs e, CancellationToken cancellationToken);
    public event NotificationNeededAsyncHandler NotificationNeededAsync;
    
    public LoanNotificationService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var intervalHours = _configuration.GetValue<int>("NotificationSettings:IntervalHours");
            
            await CheckAndSendNotificationsAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromHours(intervalHours), stoppingToken);
        }
    }

    private async Task CheckAndSendNotificationsAsync(CancellationToken cancellationToken = default)
    {
        var noticePeriodDay = _configuration.GetValue<int>("NotificationSettings:NoticePeriodDay");
        
        var soonToBeReturnedLoans = await _unitOfWork.GetCustomRepository<ILoanRepository>()
            .GetLoansDueForReturnAsync(DateTime.UtcNow.AddDays(noticePeriodDay), cancellationToken);

        foreach (var loan in soonToBeReturnedLoans)
        {
            var user = await _unitOfWork.GetBaseRepository<UserEntity>().GetByIdAsync(loan.UserId, cancellationToken);
            var book = await _unitOfWork.GetBaseRepository<BookEntity>().GetByIdAsync(loan.BookId, cancellationToken);

            if (user == null || book == null)
            {
                continue;
            }
            
            if (NotificationNeededAsync != null)
            {
                await NotificationNeededAsync(new NotificationEventArgs(){ Book = book, User = user, Loan = loan }, cancellationToken);
            }
        }
    }
}