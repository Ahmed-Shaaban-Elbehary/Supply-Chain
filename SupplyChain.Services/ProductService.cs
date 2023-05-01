using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Interfaces;

namespace SupplyChain.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.AddAsync(product);
            _unitOfWork.Commit();

        }

        public async Task UpdateProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();
        }

        public async Task DeleteProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Commit();
        }
    }
}
