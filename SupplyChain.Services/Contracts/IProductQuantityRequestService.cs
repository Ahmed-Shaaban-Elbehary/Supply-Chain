using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IProductQuantityRequestService
    {
        Task<IEnumerable<ProductQuantityRequest>> GetAllProductQuantityRequestsAsync();
        Task<IEnumerable<ProductQuantityRequest>> GetAllPagedProductQuantityRequestsAsync(int page, int pageSize);
        Task<ProductQuantityRequest> GetProductQuantityRequestByIdAsync(int id);
        Task<int> CreateProductQuantityRequestAsync(ProductQuantityRequest productQuantityRequest);
        Task<int> UpdateProductQuantityRequestAsync(ProductQuantityRequest productQuantityRequest);
        Task<int> DeleteProductQuantityRequestAsync(ProductQuantityRequest productQuantityRequest);
        Task<int> CountProductQuantityAsync();
        Task RollbackTransaction();
    }
}
