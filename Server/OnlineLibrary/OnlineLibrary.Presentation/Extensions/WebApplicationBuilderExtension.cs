using Microsoft.EntityFrameworkCore;
using OnlineLibrary.DAL.Infrastructure;
using OnlineLibrary.DAL.Infrastructure.Implementations;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Implementations;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.Presentation.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        string? dataBaseConnection = builder.Configuration.GetConnectionString("ConnectionString");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(dataBaseConnection);
        });
    }
}