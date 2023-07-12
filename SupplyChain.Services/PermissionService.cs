using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<int> CountRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllPagedRolesAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetPermissionRolesAsync(int permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetRoleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }
    }
}
