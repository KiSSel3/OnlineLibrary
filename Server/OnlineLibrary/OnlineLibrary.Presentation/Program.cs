using OnlineLibrary.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddAuthentication();
builder.AddDataBase();
builder.AddMapping();
builder.AddServices();
builder.AddValidation();
builder.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
