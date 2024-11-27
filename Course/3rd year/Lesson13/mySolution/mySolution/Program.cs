using Microsoft.EntityFrameworkCore;
using mySolution.Manager.Implementation;
using mySolution.Manager.Interface;
using mySolution.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite());
        

var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));

var taskContext = new DataContext(optionsBuilder.Options);


builder.Services.AddSingleton<IOrderManager>(provider => 
{
    taskContext.Database.EnsureCreated();
    IOrderManager om = new OrderManager(taskContext);
    return om;
});

builder.Services.AddSingleton<IUserManager>(provider =>
{
    IUserManager um = new UserManager(taskContext);
    return um;
});

builder.Services.AddSingleton<IProductManager>(provider => 
{
    IProductManager pm = new ProductManager(taskContext);
    return pm;
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