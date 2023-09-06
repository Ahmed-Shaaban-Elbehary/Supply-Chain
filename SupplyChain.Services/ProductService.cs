using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var list = await _unitOfWork.ProductRepository.GetWhereAsync(e => e.Deleted == false);
            return list;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }

        public async Task DeleteProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.RemoveAsync(product);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }

        public async Task<IEnumerable<Product>> GetAllPagedProductsAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.ProductRepository
               .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public async Task<int> CountProductsAsync()
        {
            return await _unitOfWork.ProductRepository.CountAsync();
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }
    }
}
