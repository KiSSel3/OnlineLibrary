using Microsoft.EntityFrameworkCore;
using OnlineLibrary.DAL.Infrastructure.Configurations;
using OnlineLibrary.DAL.Infrastructure.Extensions;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.DAL.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public DbSet<LoanEntity> Loans { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleEntity> UsersRoles { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new GenreEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
        modelBuilder.ApplyConfiguration(new LoanEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
        
        modelBuilder.SeedUsersRolesData();
    }
}