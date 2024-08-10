namespace OnlineLibrary.BLL.Infrastructure.Services.Interfaces;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
}