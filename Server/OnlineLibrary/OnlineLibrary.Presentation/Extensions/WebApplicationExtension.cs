using OnlineLibrary.Presentation.Middleware;

namespace OnlineLibrary.Presentation.Extensions;

public static class WebApplicationExtension
{
    public static void AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
    public static void AddApplicationMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors(builder =>
        {
            builder.WithOrigins("http://localhost:4200") 
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination")
                .AllowCredentials();
        }); 
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
    }
}