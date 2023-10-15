﻿using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Core.Interfaces
{
    public interface IProductQuantityRequestRepository : IGenericRepository<ProductQuantityRequest>
    {
        Task<int> AddProductQuantityRequestAsync(ProductQuantityRequest productQuantityRequest);
        Task<IEnumerable<ProductQuantityRequest>> ExecSqlQuery(string sql, params object[] parameters);
    }
}
