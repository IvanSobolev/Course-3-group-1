using mySolution.Models;

namespace mySolution.Manager.Interface;

public interface IUserManager
{
    public Task<List<User>> GetAllUserAsync();
    public Task<User?> GetUserByIdAsync(int id);
    public Task AddUserAsync(User customer);
    public Task UpdateUserAsync(User customer);
    public Task DeleteUserAsync(int id);
}