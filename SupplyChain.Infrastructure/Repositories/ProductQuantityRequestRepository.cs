using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Infrastructure.Repositories
{
    public class ProductQuantityRequestRepository : GenericRepository<ProductQuantityRequest>, IProductQuantityRequestRepository
    {
        private readonly SupplyChainDbContext _context;
        public ProductQuantityRequestRepository(SupplyChainDbContext dbContext) : base(dbContext) { _context = dbContext; }
        public async Task<int> AddProductQuantityRequestAsync(ProductQuantityRequest productQuantityRequest)
        {
            await _context.ProductQuantityRequests.AddAsync(productQuantityRequest);
            await _context.SaveChangesAsync();
            var property = productQuantityRequest.GetType().GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException("The entity does not have an 'Id' property.");

            return (int)(property?.GetValue(productQuantityRequest) ?? 0);
        }
        public async Task<IEnumerable<ProductQuantityRequest>> ExecSqlQuery(string sql, params object[] parameters)
        {
            return await _context.ProductQuantityRequests.FromSqlRaw(sql, parameters).ToListAsync();
        }
    }
}
