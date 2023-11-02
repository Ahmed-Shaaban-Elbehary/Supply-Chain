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
        UserSession CurrentUser { get; }
        Task SetUserAsync(User user);
        Task<bool> IsUserLoggedInAsync();
        Task<bool> HasPermissionAsync(string permissionName);
        Task<bool> IsInRoleAsync(string roleName);
        Task SetLoggedInUserRoles(List<string> userRoles);
        Task SetLoggedInUserPermissions(List<string> userPermissions);
        Task ClearUserSessionAsync();
    }
}
