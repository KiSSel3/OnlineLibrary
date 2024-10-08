namespace OnlineLibrary.Domain.Entities;

public class UserEntity : BaseEntity
{
    public string Login { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}