using System.Reflection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.BLL.Infrastructure.Services.Implementation;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.BLL.UseCases.Implementation.Author;
using OnlineLibrary.BLL.UseCases.Implementation.Book;
using OnlineLibrary.BLL.UseCases.Implementation.Genre;
using OnlineLibrary.BLL.UseCases.Implementation.Loan;
using OnlineLibrary.BLL.UseCases.Implementation.Role;
using OnlineLibrary.BLL.UseCases.Implementation.User;
using OnlineLibrary.BLL.UseCases.Interfaces.Author;
using OnlineLibrary.BLL.UseCases.Interfaces.Book;
using OnlineLibrary.BLL.UseCases.Interfaces.Genre;
using OnlineLibrary.BLL.UseCases.Interfaces.Loan;
using OnlineLibrary.BLL.UseCases.Interfaces.Role;
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
        
        //Role use cases
        builder.Services.AddScoped<ICheckUserHasRoleUseCase, CheckUserHasRoleUseCase>();
        builder.Services.AddScoped<IGetAllRolesUseCase, GetAllRolesUseCase>();
        builder.Services.AddScoped<IGetRolesByUserIdUseCase, GetRolesByUserIdUseCase>();
        builder.Services.AddScoped<IRemoveRoleFromUserUseCase, RemoveRoleFromUserUseCase>();
        builder.Services.AddScoped<ISetRoleToUserUseCase, SetRoleToUserUseCase>();
        
        //Loan use cases
        builder.Services.AddScoped<ICheckIsBookAvailableUseCase, CheckIsBookAvailableUseCase>();
        builder.Services.AddScoped<ICreateNewLoanUseCase, CreateNewLoanUseCase>();
        builder.Services.AddScoped<IDeleteLoanByBookIdUseCase, DeleteLoanByBookIdUseCase>();
        builder.Services.AddScoped<IGetLoansByUserIdUseCase, GetLoansByUserIdUseCase>();
        
        //Genre use cases
        builder.Services.AddScoped<ICreateNewGenreUseCase, CreateNewGenreUseCase>();
        builder.Services.AddScoped<IDeleteGenreUseCase, DeleteGenreUseCase>();
        builder.Services.AddScoped<IGetAllGenresUseCase, GetAllGenresUseCase>();
        builder.Services.AddScoped<IGetGenreByIdUseCase, GetGenreByIdUseCase>();
        builder.Services.AddScoped<IUpdateGenreUseCase, UpdateGenreUseCase>();
        
        //Book use cases
        builder.Services.AddScoped<ICreateNewBookUseCase, CreateNewBookUseCase>();
        builder.Services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
        builder.Services.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
        builder.Services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
        builder.Services.AddScoped<IGetBookByIsbnUseCase, GetBookByIsbnUseCase>();
        builder.Services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
        
        //Author use cases
        builder.Services.AddScoped<ICreateNewAuthorUseCase, CreateNewAuthorUseCase>();
        builder.Services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
        builder.Services.AddScoped<IGetAllAuthorsUseCase, GetAllAuthorsUseCase>();
        builder.Services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
        builder.Services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
    }
    
    public static void AddMapping(this WebApplicationBuilder builder)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        builder.Services.AddMapster();
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