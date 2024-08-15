using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.Domain.Models;

public class EmailNotificationService : INotificationService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;
    private readonly string _fromEmail;
    private readonly ILogger<EmailNotificationService> _logger;

    public EmailNotificationService(IConfiguration configuration, ILogger<EmailNotificationService> logger)
    {
        _logger = logger;
        
        _smtpServer = configuration["SmtpSettings:Server"];
        _smtpPort = configuration.GetValue<int>("SmtpSettings:Port");
        _smtpUser = configuration["SmtpSettings:Username"];
        _smtpPass = configuration["SmtpSettings:Password"];
        _fromEmail = configuration["SmtpSettings:FromEmail"];
    }

    public async Task SendNotificationAsync(NotificationEventArgs e, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(e);
        ArgumentNullException.ThrowIfNull(e.Book);
        ArgumentNullException.ThrowIfNull(e.User);
        ArgumentNullException.ThrowIfNull(e.Loan);

        var message = new MimeMessage
        {
            From = { new MailboxAddress("Online Library", _fromEmail) },
            To = { new MailboxAddress(e.User.Login, e.User.Email) },
            Subject = "Book Return Reminder",
            Body = new TextPart("plain")
            {
                Text = $"Dear {e.User.Login},\n\n" +
                       $"This is a reminder that the book '{e.Book.Title}' (ISBN: {e.Book.ISBN}) you borrowed is due for return on {e.Loan.ReturnBy.ToShortDateString()}.\n" +
                       $"Please return it by the due date to avoid any late fees.\n\n" +
                       "Thank you,\nOnline Library"
            }
        };

        try
        {
            using (var client = new SmtpClient())
            {
                _logger.LogInformation("Connecting to SMTP server {Server} on port {Port}.", _smtpServer, _smtpPort);
                await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);
                
                _logger.LogInformation("Authenticating with SMTP server.");
                await client.AuthenticateAsync(_smtpUser, _smtpPass, cancellationToken);
                
                _logger.LogInformation("Sending email.");
                await client.SendAsync(message, cancellationToken);
                
                _logger.LogInformation("Disconnecting from SMTP server.");
                await client.DisconnectAsync(true, cancellationToken);

                _logger.LogInformation("Email sent successfully to {Recipient}.", e.User.Email);
            }
        }
        catch (SmtpCommandException ex)
        {
            _logger.LogError(ex, "SMTP command error while sending email.");
        }
        catch (SmtpProtocolException ex)
        {
            _logger.LogError(ex, "SMTP protocol error while sending email.");
        }
        catch (AuthenticationException ex)
        {
            _logger.LogError(ex, "SMTP authentication error while sending email. Check credentials.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "General error sending email.");
        }
    }
}
