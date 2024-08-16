using OnlineLibrary.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.AddAuthentication();
builder.AddDataBase();
builder.AddMapping();
builder.AddServices();
builder.AddValidation();

builder.AddSwaggerDocumentation();

builder.AddNotification();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddApplicationMiddleware();
app.AddSwagger();

app.Run();
