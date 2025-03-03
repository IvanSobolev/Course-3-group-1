using UserManagment.Models;
using UserManagment.Repository.Interface;
using UserManagment.Services.Interface;

namespace UserManagment.Services.Implementation;

public class UserService(IUserRepository userRepository, IEmailService emailService) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailService _emailService = emailService;
    

    public async Task AddUserAsync(string username, string email)
    {
        var user = new User() { Username = username, Email = email };
        await _userRepository.AddUserAsync(user);
        await _emailService.SendEmailAsync(email, "Welcome!");
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<User?> GetUserAsync(int userId)
    {
        return await _userRepository.GetUserAsync(userId);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }
}