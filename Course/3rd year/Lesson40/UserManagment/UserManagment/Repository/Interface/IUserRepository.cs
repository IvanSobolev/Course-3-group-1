using UserManagment.Models;

namespace UserManagment.Repository.Interface;

public interface IUserRepository
{
    Task AddUserAsync(User? user);
    Task DeleteUserAsync(int userId);
    Task<User?> GetUserAsync(int userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
}