using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IUserSessionService
    {
        Task<User?> GetUserAsync();
        Task SetUserAsync(User user);
        Task<bool> IsUserLoggedInAsync();
        Task<bool> HasPermissionAsync(string permission);
        Task<IEnumerable<string>> GetUserPermissionsAsync();
        Task<IEnumerable<string>> GetUserRolesAsync();
        Task<bool> IsInRoleAsync(string roleName);
        Task LoginAsync(User user);
        Task LogoutAsync();
    }
}
