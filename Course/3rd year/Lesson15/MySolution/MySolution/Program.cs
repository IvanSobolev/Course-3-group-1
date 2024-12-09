using Microsoft.EntityFrameworkCore;
using MySolution.Model;
using MySolution.Service.Implementation;
using MySolution.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite());
builder.Services.AddDbContext<LogDataContext>(options => options.UseSqlite());

var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("SQLiteData"));

var dataContext = new DataContext(optionsBuilder.Options);

builder.Services.AddSingleton<INoteRepository>(provider => 
{
    dataContext.Database.EnsureCreated();
    INoteRepository noteRepository = new SqliteNoteRepository(dataContext);
    return noteRepository;
});

var optionsLogBuilder = new DbContextOptionsBuilder<LogDataContext>();
optionsLogBuilder.UseSqlite(builder.Configuration.GetConnectionString("SQLiteLog"));

var logDataContext = new LogDataContext(optionsLogBuilder.Options);

builder.Services.AddSingleton<ILoggerServices>(provider =>
{
    logDataContext.Database.EnsureCreated();
    ILoggerServices noteRepository = new SqliteLoggerService(logDataContext);
    return noteRepository;
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
