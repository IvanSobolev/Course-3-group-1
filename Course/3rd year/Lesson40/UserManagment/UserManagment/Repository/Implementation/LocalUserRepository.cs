using UserManagment.Models;
using UserManagment.Repository.Interface;

namespace UserManagment.Repository.Implementation;

public class LocalUserRepository : IUserRepository
{
    private List<User?> _users = new List<User?>();
    
    public Task AddUserAsync(User? user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            _users.Remove(user);
        }

        return Task.CompletedTask;
    }

    public Task<User?> GetUserAsync(int userId)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == userId));
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return Task.FromResult<IEnumerable<User>>(_users);
    }
}