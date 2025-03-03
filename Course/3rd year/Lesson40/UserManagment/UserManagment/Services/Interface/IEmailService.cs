namespace UserManagment.Services.Interface;

public interface IEmailService
{
    Task SendEmailAsync(string email, string msg);
}