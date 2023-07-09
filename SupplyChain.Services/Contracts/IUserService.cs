using SupplyChain.Core.Models;

namespace SupplyChain.Services.Contracts
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId);
        Task<int> CreateUserAsync(User user, string password);
        Task<int> UpdateUserAsync(User user);
        Task<int> DeleteUserAsync(User User);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
        Task<IEnumerable<string>> GetUserPermissionsAsync(int userId);
        Task<IEnumerable<User>> GetAllPagedUsersAsync(int page, int pageSize);
        Task<int> CountUserAsync();
    }
}
