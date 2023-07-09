using SupplyChain.Core.Models;

namespace SupplyChain.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<string>> GetUserPermissionsAsync(int userId);
    }
}
