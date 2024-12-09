using mySolution.Models;

namespace mySolution.Manager.Interface;

public interface IOrderManager
{
    public Task<List<Order>> GetAllOrdersAsync();
    public Task<Order?> GetOrderByIdAsync(int id);
    public Task AddOrderAsync(Order order);
    public Task UpdateOrderAsync(Order order);
    public Task DeleteOrderAsync(int id);
}