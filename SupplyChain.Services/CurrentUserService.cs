using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class CurrentUserService
    {
        private static int UserId { get; set; }
        private static string UserName { get; set; } = string.Empty;
        private static IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
        private static IEnumerable<string> Permissions { get; set; } = Enumerable.Empty<string>();
        public static async Task SetUserAsync(User user, IUserService _userService)
        {
            UserId = user.Id;
            UserName = user.Name;
            Roles = await _userService.GetUserRolesAsync(UserId);
            Permissions = await _userService.GetUserPermissionsAsync(UserId);
        }
        public static Task<bool> IsInRoleAsync(string roleName)
        {
            return Task.Run(() => Roles.Contains(roleName));
        }
        public static Task<bool> HasPermissionAsync(string permissionName)
        {
            return Task.Run(() => Permissions.Contains(permissionName));
        }
    }
}
