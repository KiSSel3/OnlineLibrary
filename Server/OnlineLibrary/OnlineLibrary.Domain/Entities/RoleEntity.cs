namespace OnlineLibrary.Domain.Entities;

public class RoleEntity : BaseEntity
{
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}