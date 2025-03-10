using Microsoft.EntityFrameworkCore;
using Solution.Model.Entity;
using Solution.Model.Structures;
using Solution.Repository.Interface;

namespace Solution.Repository.Implementation;

public class LocalUserRepository : IUserRepository
{
    private readonly List<User> _users = new List<User>();
    
    public Task<OperationResult> AddNewUserAsync(string login, string paswordHash)
    {
        try
        {
            _users.Add(new User() { Id = Guid.NewGuid(), Login = login, PasswordHash = paswordHash});
            return Task.FromResult(new OperationResult(true));
        }
        catch (Exception ex)
        {
            return Task.FromResult(new OperationResult(false, ex.Message));
        }
    }

    public Task<User?> GetUserAsync(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == Guid.Parse(id));

        return Task.FromResult(user);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize, string filterLogin = "", string filterPasswordHash = "",
        bool sortedByLogin = false)
    {
        var query = _users.AsQueryable();
        if (!string.IsNullOrEmpty(filterLogin))
        {
            query = query.Where(u => u.Login.Contains(filterLogin));
        }
        
        if (!string.IsNullOrEmpty(filterPasswordHash))
        {
            query = query.Where(u => u.Login.Contains(filterPasswordHash));
        }

        if (sortedByLogin)
        {
            query = query.OrderBy(u => u.Login);
        }
        
        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public Task<OperationResult> UpdateUserAsync(string id, string? newLogin, string? newPasswordHash)
    {
        var user = _users.FirstOrDefault(u => u.Id == Guid.Parse(id));
        if (user == null)
        {
            return Task.FromResult(new OperationResult(false, "User is empty"));
        }
        
        if (newLogin != null)
        {
            user.Login = newLogin;
        }
        if (newPasswordHash != null)
        {
            user.PasswordHash = newPasswordHash;
        }

        return Task.FromResult(new OperationResult(true));
    }

    public Task<OperationResult> DeleteUserAsync(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == Guid.Parse(id));
        if (user == null)
        {
            return Task.FromResult(new OperationResult(false, "User is empty"));
        }
        
        try
        {
            _users.Remove(user);
            return Task.FromResult(new OperationResult(true));
        }
        catch (Exception ex)
        {
            return Task.FromResult(new OperationResult(false, ex.Message));
        }
    }
}