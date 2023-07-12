using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RolePermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task AddRolePermissionAsync(int roleId, List<int> permissionIds)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountRolePermissionAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteRolePermissionAsync(int roleId, int permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RolePermission>> GetAllPagedRolePermissionAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RolePermission>> GetAllRolePermissionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<RolePermission>> GetRolePermissionsByRoleIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRolePermissionAsync(int roleId, List<int> permissionIds)
        {
            throw new NotImplementedException();
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }
    }
}
