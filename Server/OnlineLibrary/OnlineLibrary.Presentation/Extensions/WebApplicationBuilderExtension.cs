using Microsoft.EntityFrameworkCore;
using OnlineLibrary.DAL.Infrastructure;

namespace OnlineLibrary.Presentation.Extensions;

public static class WebApplicationBuilderExtension
{
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