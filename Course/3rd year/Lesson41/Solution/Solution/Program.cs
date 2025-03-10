using Microsoft.EntityFrameworkCore;
using Solution.EFCore;
using Solution.Model.Entity;
using Solution.Repository.Implementation;
using Solution.Repository.Interface;
using Solution.Service.Implementation;
using Solution.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserManager, UserManager>();

builder.Services.AddScoped<IUserRepository, EfCoreUserRepository>();

builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteData"));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
