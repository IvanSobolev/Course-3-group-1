using Microsoft.EntityFrameworkCore;
using ToDo.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLite")));

var app = builder.Build();

using var servicecope = app.Services.CreateScope();

try
{
    var appContextService = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

    appContextService.Database.Migrate();
}
catch (Exception exc)
{
    app.Logger.LogError(message: "Ошибка миграции базы данных: {exc}", exc);
    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();