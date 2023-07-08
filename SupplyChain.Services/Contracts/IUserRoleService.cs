using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllUserRoleAsync();
        Task<IEnumerable<UserRole>> GetAllPagedUserRoleAsync(int page, int pageSize);
        Task<List<UserRole>> GetUserRolesByUserIdAsync(int userId);
        Task<List<UserRole>> GetUserRolesByRoleIdAsync(int roleId);
        Task AddUserRolesAsync(int userId, List<int> roleIds);
        Task DeleteUserRoleAsync(int userId, int roleId);
        Task UpdateUserRolesAsync(int userId, List<int> roleIds);
        Task<int> CountUserRolesAsync();
    }
}
