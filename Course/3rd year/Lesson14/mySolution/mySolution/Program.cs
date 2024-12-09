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

var dataContext = new DataContext(optionsBuilder.Options);

builder.Services.AddSingleton<IOrderManager>(provider =>
{
    dataContext.Database.EnsureCreated();
    IOrderManager orderManager = new OrderManager(dataContext);
    return orderManager;
});

builder.Services.AddSingleton<IUserManager>(provider =>
{
    dataContext.Database.EnsureCreated();
    IUserManager userManager = new UserManager(dataContext);
    return userManager;
});

builder.Services.AddSingleton<IProductManager>(provider =>
{
    dataContext.Database.EnsureCreated();
    IProductManager productManager = new ProductManager(dataContext);
    return productManager;
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