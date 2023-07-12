using SupplyChain.Core.Models;

namespace SupplyChain.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllPagedProductsAsync(int page, int pageSize);
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<int> CountProductsAsync();
        Task RollbackTransaction();
    }
}
