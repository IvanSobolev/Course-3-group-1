using MySolution.Manager.Interfaces;
using MySolution.Manager.Implementations;
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
        
        builder.Services.AddSingleton<ITaskManager>(provider => 
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));

            var taskContext = new DataContext(optionsBuilder.Options);
            taskContext.Database.EnsureCreated();

            ITaskManager tm = new TaskManager(taskContext);
            
            return tm;
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