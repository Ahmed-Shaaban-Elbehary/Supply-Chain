using SupplyChain.Core.Models;

namespace SupplyChain.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<int> AddUserAsync(User user);
        Task<IEnumerable<string>> GetUserPermissionsAsync(int userId);
    }
}
