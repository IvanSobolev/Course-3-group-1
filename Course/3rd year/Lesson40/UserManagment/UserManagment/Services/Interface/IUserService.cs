using UserManagment.Models;

namespace UserManagment.Services.Interface;

public interface IUserService
{
    Task AddUserAsync(string username, string email);
    Task DeleteUserAsync(int userId);
    Task<User?> GetUserAsync(int userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
}