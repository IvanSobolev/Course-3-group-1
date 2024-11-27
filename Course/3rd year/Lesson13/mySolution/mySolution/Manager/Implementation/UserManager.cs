using Microsoft.EntityFrameworkCore;
using mySolution.Manager.Interface;
using mySolution.Models;

namespace mySolution.Manager.Implementation;

public class UserManager(DataContext context) : IUserManager
{
    private readonly DataContext _context = context;

    public async Task<List<User>> GetAllUserAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddUserAsync(User customer)
    {
        _context.Users.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User customer)
    {
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var customer = await _context.Users.FindAsync(id);
        if (customer != null)
        {
            _context.Users.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}