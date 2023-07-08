using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<IEnumerable<Role>> GetAllPagedRolesAsync(int page, int pageSize);
        Task<Role> GetRoleByIdAsync(int id);
        Task CreateRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(Role role);
        Task<int> CountRoleAsync();
        Task<IEnumerable<User>> GetRoleUsersAsync(int roleId);
        Task<IEnumerable<Permission>> GetRolePermissionsAsync(int roleId);
    }
}
