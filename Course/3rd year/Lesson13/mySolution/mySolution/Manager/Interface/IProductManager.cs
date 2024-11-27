using mySolution.Models;

namespace mySolution.Manager.Interface;

public interface IProductManager
{
    public Task<List<Product>> GetAllProductsAsync();
    public Task<Product> GetProductByIdAsync(int id);
    public Task AddProductAsync(Product product);
    public Task UpdateProductAsync(Product product);
    public Task DeleteProductAsync(int id);
}