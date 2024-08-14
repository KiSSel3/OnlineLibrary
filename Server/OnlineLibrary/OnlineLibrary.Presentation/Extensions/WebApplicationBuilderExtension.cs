using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.BLL.Infrastructure.Services.Implementation;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.BLL.Infrastructure.Validators;
using OnlineLibrary.BLL.UseCases.Implementation.User;
using OnlineLibrary.BLL.UseCases.Interfaces.User;
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

        //Repositories
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        
        //Services
        builder.Services.AddScoped<IPasswordService, PasswordService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        
        //User use cases
        builder.Services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
        builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
        builder.Services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
        builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();
        builder.Services.AddScoped<IRevokeUseCase, RevokeUseCase>();
    }
    
    public static void AddMapping(this WebApplicationBuilder builder)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        builder.Services.AddMapster();
    }
    
    public static void AddValidation(this WebApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddValidatorsFromAssemblyContaining<AuthorDTOValidator>();
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