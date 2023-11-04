using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System.Text;

namespace SupplyChain.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string UserSessionKey { get; set; } = string.Empty;
        private List<string> userPermissions = new List<string>();
        private List<string> userRoles = new List<string>();

        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserSession CurrentUser { get; set; } = new UserSession();

        public async Task<bool> HasPermissionAsync(string permissionName)
        {
            return await Task.FromResult(this.userPermissions.Contains(permissionName));
        }

        public async Task<bool> IsInRoleAsync(string roleName)
        {
            return await Task.FromResult(this.userRoles.Contains(roleName));
        }

        public async Task<bool> IsUserLoggedInAsync()
        {
            byte[] userData;
            return await Task.FromResult(_httpContextAccessor.HttpContext.Session.TryGetValue(this.UserSessionKey, out userData));
        }

        public async Task SetUserAsync(User user)
        {
            var _user = new UserSession
            {
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email,
                IsSupplier = user.IsSupplier
            };
            var userJson = JsonConvert.SerializeObject(_user);
            this.UserSessionKey = $"UserData_{user.Id}";
            _httpContextAccessor.HttpContext.Session.Set(this.UserSessionKey, Encoding.UTF8.GetBytes(userJson));
            this.CurrentUser = _user;
            await Task.CompletedTask;
        }

        public async Task ClearUserSessionAsync()
        {
            _httpContextAccessor.HttpContext.Session.Remove("CurrentUser");
            await Task.CompletedTask;
        }

        public async Task SetLoggedInUserRoles(List<string> userRoles)
        {
            this.userRoles = userRoles;
            await Task.CompletedTask;
        }

        public async Task SetLoggedInUserPermissions(List<string> userPermissions)
        {
            this.userPermissions = userPermissions;
            await Task.CompletedTask;
        }

    }

    public record UserSession
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsSupplier { get; set; }
    }
}
