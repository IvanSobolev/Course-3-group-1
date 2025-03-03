using UserManagment.Repository.Implementation;
using UserManagment.Repository.Interface;
using UserManagment.Services.Implementation;
using UserManagment.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<IUserRepository, LocalUserRepository>();
builder.Services.AddSingleton<IEmailService, ConsoleEmailService>();
builder.Services.AddScoped<IUserService, UserService>();

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