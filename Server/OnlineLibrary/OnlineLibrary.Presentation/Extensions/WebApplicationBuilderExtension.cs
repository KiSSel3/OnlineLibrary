using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineLibrary.BLL.Infrastructure.BackgroundServices;
using OnlineLibrary.BLL.Infrastructure.Mappers;
using OnlineLibrary.BLL.Infrastructure.Services.Implementation;
using OnlineLibrary.BLL.Infrastructure.Services.Interfaces;
using OnlineLibrary.BLL.UseCases.Implementation.Author;
using OnlineLibrary.BLL.UseCases.Implementation.Book;
using OnlineLibrary.BLL.UseCases.Implementation.Genre;
using OnlineLibrary.BLL.UseCases.Implementation.Loan;
using OnlineLibrary.BLL.UseCases.Implementation.Role;
using OnlineLibrary.BLL.Infrastructure.Validators;
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

        builder.Services.AddMemoryCache();
        
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
    
    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");

        var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
        });
        
        builder.Services.AddAuthorization(options => options.DefaultPolicy =
            new AuthorizationPolicyBuilder
                    (JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
        
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminArea", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Admin");
            });
        });
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
    
    public static void AddSwaggerDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description = @"Enter JWT Token please.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                }
            );
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                }
            );
        });
    }

    public static void AddNotification(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<INotificationService, EmailNotificationService>();
        builder.Services.AddHostedService<LoanNotificationService>();
    }
    
    public static void AddMapping(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(GetConfiguredMappingConfig());
        builder.Services.AddScoped<IMapper, ServiceMapper>();
    }
    private static TypeAdapterConfig GetConfiguredMappingConfig()
    {
        var config = new TypeAdapterConfig();

        new RegisterMapper().Register(config);
       
        return config;
    }
}