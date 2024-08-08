namespace OnlineLibrary.Domain.Entities;

public class UserRoleEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public Guid IsDeleted { get; set; }
}