using MySolution.Models;

namespace MySolution;
using Microsoft.EntityFrameworkCore;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<DataContext>(options => options.UseSqlite());
        
        builder.Services.AddSingleton<DataContext>(provider => 
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));

            var taskContext = new DataContext(optionsBuilder.Options);
            taskContext.Database.EnsureCreated();

            return taskContext;
        });
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.MapControllers();
        
        app.Run();
    }
}