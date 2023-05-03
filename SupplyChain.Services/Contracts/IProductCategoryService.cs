using SupplyChain.Core.Models;

namespace SupplyChain.Services.Contracts
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync();
        Task<ProductCategory> GetProductCategoryByIdAsync(int id);
        Task CreateProductCategoryAsync(ProductCategory product);
        Task UpdateProductCategoryAsync(ProductCategory product);
        Task DeleteProductCategoryAsync(ProductCategory product);
    }
}
