using SupplyChain.Core.Models;

namespace SupplyChain.Services.Contracts
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync();
        Task<IEnumerable<ProductCategory>> GetAllPagedProductCategoriesAsync(int page, int pageSize);
        Task<ProductCategory> GetProductCategoryByIdAsync(int id);
        Task CreateProductCategoryAsync(ProductCategory productCategory);
        Task UpdateProductCategoryAsync(ProductCategory productCategory);
        Task DeleteProductCategoryAsync(ProductCategory productCategory);
        Task<int> CountProductCategoryAsync();
    }
}
