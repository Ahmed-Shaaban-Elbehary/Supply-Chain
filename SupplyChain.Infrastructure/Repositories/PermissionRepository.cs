using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Infrastructure.Repositories
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(SupplyChainDbContext dbContext) : base(dbContext) { }
    }
}
