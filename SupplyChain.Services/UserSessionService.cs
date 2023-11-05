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

        private List<string> userPermissions = new List<string>();
        private List<string> userRoles = new List<string>();

        // Private field to store the user ID
        private int _userId = -1;

        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserSession CurrentUser
        {
            get
            {
                byte[] userData;
                if (_httpContextAccessor.HttpContext.Session.TryGetValue(UserSessionKey, out userData))
                {
                    return Deserialize<UserSession>(userData);
                }
                return new UserSession();
            }
            set
            {
                var userJson = Serialize(value);
                _userId = value.UserId;
                _httpContextAccessor.HttpContext.Session.Set(UserSessionKey, userJson);
            }
        }

        private string UserSessionKey => $"UserData_{_userId}";

        public async Task<bool> HasPermissionAsync(string permissionName)
        {
            return await Task.FromResult(this.userPermissions.Contains(permissionName));
        }

        public async Task<bool> IsInRoleAsync(string roleName)
        {
            return await Task.FromResult(this.userRoles.Contains(roleName));
        }

        public async Task<bool> IsUserLoggedInAsync(int currentUserId)
        {
            byte[] userData;
            UserSession userSession;
            bool isLoggedIn = false;
            if (await Task.FromResult(_httpContextAccessor.HttpContext.Session.TryGetValue(UserSessionKey, out userData)))
            {
                userSession = Deserialize<UserSession>(userData);
                isLoggedIn = userSession.UserId == currentUserId;
            }
            return isLoggedIn;
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
            this._userId = user.Id;
            this.CurrentUser = _user;
            await Task.CompletedTask;
        }

        public async Task ClearUserSessionAsync()
        {
            _httpContextAccessor.HttpContext.Session.Remove(UserSessionKey);
            this.CurrentUser = new UserSession(); // Clear the current user
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

        // Helper methods for serializing and deserializing data
        private byte[] Serialize<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(json);
        }

        private T Deserialize<T>(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
#pragma warning disable CS8603 // Possible null reference return.
            return JsonConvert.DeserializeObject<T>(json);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<bool> IsUserLoggedInAsync()
        {
            byte[] userData;
            bool HasValue = await Task.FromResult(_httpContextAccessor.HttpContext.Session.TryGetValue(this.UserSessionKey, out userData));
            return HasValue;
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
