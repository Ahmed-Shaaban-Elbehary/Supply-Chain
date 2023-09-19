﻿using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Infrastructure.Repositories
{
    public class ProductEventRepository : GenericRepository<ProductEvent>, IProductEventRepository
    {
        private readonly SupplyChainDbContext _dbContext;
        public ProductEventRepository(SupplyChainDbContext dbContext) : base(dbContext) { _dbContext = dbContext; }
    }
}
