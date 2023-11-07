using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class ProductQuantityRequestService : IProductQuantityRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductQuantityRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountEventAsync()
        {
            return await _unitOfWork.ProductQuantityRequestRepository.CountAsync();
        }

        public async Task<int> CreateProductQuantityRequestAsync(ProductQuantityRequest productQuantityRequest)
        {
            try
            {
                await _unitOfWork.ProductQuantityRequestRepository.AddProductQuantityRequestAsync(productQuantityRequest);
                var result = await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransaction();
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<int> DeleteProductQuantityRequestAsync(ProductQuantityRequest productQuantityRequest)
        {
            try
            {
                await _unitOfWork.ProductQuantityRequestRepository.RemoveAsync(productQuantityRequest);
                var result = await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransaction();
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ProductQuantityRequest>> GetAllPagedProductQuantityRequestsAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.ProductQuantityRequestRepository
               .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public async Task<IEnumerable<ProductQuantityRequest>> GetAllProductQuantityRequestsAsync()
        {
            return await _unitOfWork.ProductQuantityRequestRepository.GetAllAsync();
        }

        public async Task<ProductQuantityRequest> GetProductQuantityRequestByIdAsync(int id)
        {
            return await _unitOfWork.ProductQuantityRequestRepository.GetByIdAsync(id);
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }

        public async Task<int> UpdateProductQuantityRequestAsync(ProductQuantityRequest productQuantityRequest)
        {
            try
            {
                await _unitOfWork.ProductQuantityRequestRepository.UpdateAsync(productQuantityRequest);
                var result = await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransaction();
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
