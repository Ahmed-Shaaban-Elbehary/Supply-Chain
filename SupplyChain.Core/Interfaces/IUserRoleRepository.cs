using SupplyChain.Core.Models;

namespace SupplyChain.Core.Interfaces
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        Task<List<UserRole>> GetUserRolesByUserIdAsync(int userId);
        Task<List<UserRole>> GetUserRolesByRoleIdAsync(int roleId);
        Task<UserRole> GetUserRoleByUserIdAsync(int userId);
        Task<UserRole> GetUserRoleByRoleIdAsync(int roleId);
    }
}
