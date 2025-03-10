using Microsoft.EntityFrameworkCore;
using Solution.EFCore;
using Solution.Model.Entity;
using Solution.Model.Structures;
using Solution.Repository.Interface;

namespace Solution.Repository.Implementation;

public class EfCoreUserRepository(UserContext userContext) : IUserRepository
{
    private readonly UserContext _userContext = userContext;
    public async Task<OperationResult> AddNewUserAsync(string login, string paswordHash)
    {
        try
        {
            await _userContext.Users.AddAsync(new User() { Id = Guid.NewGuid(), Login = login, PasswordHash = paswordHash});
            await _userContext.SaveChangesAsync();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, ex.Message);
        }
    }

    public async Task<User?> GetUserAsync(string id)
    {
        var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(id));

        return user;
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize, string filterLogin = "", string filterPasswordHash = "",
        bool sortedByLogin = false)
    {
        var query = _userContext.Users.AsQueryable();
        
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

    public async Task<OperationResult> UpdateUserAsync(string id, string? newLogin, string? newPasswordHash)
    {
        var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(id));
        if (user == null)
        {
            return new OperationResult(false, "User is empty");
        }
        
        if (newLogin != null)
        {
            user.Login = newLogin;
        }
        if (newPasswordHash != null)
        {
            user.PasswordHash = newPasswordHash;
        }

        await _userContext.SaveChangesAsync();
        
        return new OperationResult(true);
    }

    public async Task<OperationResult> DeleteUserAsync(string id)
    {
        var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(id));
        if (user == null)
        {
            return new OperationResult(false, "User is empty");
        }
        
        try
        {
            _userContext.Remove(user);
            await _userContext.SaveChangesAsync();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, ex.Message);
        }
    }
}