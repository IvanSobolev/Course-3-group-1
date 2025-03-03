using UserManagment.Services.Interface;

namespace UserManagment.Services.Implementation;

public class ConsoleEmailService : IEmailService
{
    public Task SendEmailAsync(string email, string msg)
    {
        Console.WriteLine($"{email}: {msg}");
        return Task.CompletedTask;
    }
}