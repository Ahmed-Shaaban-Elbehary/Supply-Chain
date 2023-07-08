using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        private readonly SupplyChainDbContext _dbContext;
        public UserRoleRepository(SupplyChainDbContext dbContext) : base(dbContext) { _dbContext = dbContext; }

        public async Task<List<UserRole>> GetUserRolesByRoleIdAsync(int roleId)
        {
            return await _dbContext.UserRoles.Where(ur => ur.RoleId == roleId).ToListAsync();
        }

        public async Task<List<UserRole>> GetUserRolesByUserIdAsync(int userId)
        {
            return await _dbContext.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();
        }
    }
}
