using Microsoft.EntityFrameworkCore;
using mySolution.Manager.Interface;
using mySolution.Models;

namespace mySolution.Manager.Implementation;

public class OrderManager(DataContext context) : IOrderManager
{
    private readonly DataContext _context = context;

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}