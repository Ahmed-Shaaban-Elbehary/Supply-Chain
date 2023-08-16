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
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SupplyChainDbContext _context;
        public UserRepository(SupplyChainDbContext dbContext) : base(dbContext) { _context = dbContext; }

        public async Task<int> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var property = user.GetType().GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException("The entity does not have an 'Id' property.");

            return (int)(property?.GetValue(user) ?? 0);
        }

        public async Task<IEnumerable<string>> GetUserPermissionsAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var permissions = new List<Permission>();

            foreach (var userRole in user.UserRoles)
            {
                var rolePermissions = userRole.Role.RolePermissions.Select(rp => rp.Permission);
                permissions.AddRange(rolePermissions);
            }

            return permissions.Select(r => r.Name).ToList();
        }
    }
}
