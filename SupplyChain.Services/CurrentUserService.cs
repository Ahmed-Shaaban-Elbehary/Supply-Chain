using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class CurrentUserService : ICurrentUserSerivce
    {
        private readonly IUserService _userService;
        public CurrentUserService(IUserService userService)
        {
            _userService = userService;
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        private IEnumerable<string> Roles = Enumerable.Empty<string>();
        private IEnumerable<string> Permissions = Enumerable.Empty<string>();

        public async Task SetUserAsync(User user)
        {
            UserId = user.Id;
            UserName = user.Name;
            Roles = await _userService.GetUserRolesAsync(UserId);
            Permissions = await _userService.GetUserPermissionsAsync(UserId);
        }
        public bool IsInRole(string roleName)
        {
            return Roles.Contains(roleName);
        }
        public bool HasPermission(string permissionName)
        {
            return Permissions.Contains(permissionName);
        }
    }
}
