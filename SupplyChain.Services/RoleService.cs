using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CountRoleAsync()
        {
            return await _unitOfWork.RoleRepository.CountAsync();
        }

        public async Task<int> CreateRoleAsync(Role role)
        {
            await _unitOfWork.RoleRepository.AddAsync(role);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteRoleAsync(Role role)
        {
            await _unitOfWork.RoleRepository.RemoveAsync(role);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Role>> GetAllPagedRolesAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.UserRepository
                .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return (IEnumerable<Role>)result;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _unitOfWork.RoleRepository.GetAllAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _unitOfWork.RoleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Permission>> GetRolePermissionsAsync(int roleId)
        {
            var rolePermissions = await _unitOfWork.RolePermissionRepository.GetWhereQueryable(e => e.RoleId == roleId)
                                                                            .Include(rp => rp.Permission)
                                                                            .ToListAsync();

            return rolePermissions.Select(rp => rp.Permission);
        }

        public async Task<IEnumerable<User>> GetRoleUsersAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
