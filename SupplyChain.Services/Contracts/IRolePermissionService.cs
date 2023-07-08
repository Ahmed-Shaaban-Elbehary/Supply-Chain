using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IRolePermissionService
    {
        Task<IEnumerable<RolePermission>> GetAllRolePermissionAsync();
        Task<IEnumerable<RolePermission>> GetAllPagedRolePermissionAsync(int page, int pageSize);
        Task<List<RolePermission>> GetRolePermissionsByRoleIdAsync(int roleId);
        Task AddRolePermissionAsync(int roleId, List<int> permissionIds);
        Task DeleteRolePermissionAsync(int roleId, int permissionId);
        Task UpdateRolePermissionAsync(int roleId, List<int> permissionIds);
        Task<int> CountRolePermissionAsync();
    }
}
