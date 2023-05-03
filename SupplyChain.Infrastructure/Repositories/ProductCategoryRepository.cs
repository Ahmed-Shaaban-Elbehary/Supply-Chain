using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Infrastructure.Repositories
{
    public class ProductCategoryRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductCategoryRepository(SupplyChainDbContext dbContext) : base(dbContext) { }
    }
}
