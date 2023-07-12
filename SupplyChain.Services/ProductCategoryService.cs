using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountProductCategoryAsync()
        {
            return await _unitOfWork.ProductCategoryRepository.CountAsync();
        }

        public async Task CreateProductCategoryAsync(ProductCategory productCategory)
        {
            await _unitOfWork.ProductCategoryRepository.AddAsync(productCategory);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteProductCategoryAsync(ProductCategory productCategory)
        {
            await _unitOfWork.ProductCategoryRepository.RemoveAsync(productCategory);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync()
        {
            return await _unitOfWork.ProductCategoryRepository.GetAllAsync();
        }

        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            return await _unitOfWork.ProductCategoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            await _unitOfWork.ProductCategoryRepository.UpdateAsync(productCategory);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAllPagedProductCategoriesAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.ProductCategoryRepository
                .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }
    }
}
