using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class UserRoleService : IUserRoleService
    {
        public Task AddUserRolesAsync(int userId, List<int> roleIds)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountUserRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserRoleAsync(int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserRole>> GetAllPagedUserRoleAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserRole>> GetAllUserRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRole>> GetUserRolesByRoleIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRole>> GetUserRolesByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserRolesAsync(int userId, List<int> roleIds)
        {
            throw new NotImplementedException();
        }
    }
}
