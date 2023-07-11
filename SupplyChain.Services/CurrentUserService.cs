using Microsoft.Extensions.Caching.Memory;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System.Data;

namespace SupplyChain.Services
{
    public class CurrentUser
    {
        private static readonly MemoryCache SessionCache = new MemoryCache(new MemoryCacheOptions());
        private static int LoggedUserId = 0;

        //Cache Methods
        private static void SetSessionData(int userId, string userName, IEnumerable<string> roles, IEnumerable<string> permissions, bool isSupplier)
        {
            var sessionData = new SessionData
            {
                UserId = userId,
                UserName = userName,
                Roles = roles,
                Permissions = permissions,
                IsSupplierAccount = isSupplier
            };
            SessionCache.Set(userId.ToString(), sessionData);
        }
        private static SessionData GetSessionData(int userId)
        {
            SessionData sessionCache = SessionCache.Get(userId.ToString()) as SessionData ?? new SessionData();
            return sessionCache;
        }
        private static void ClearSessionData(int userId)
        {
            LoggedUserId = 0;
            SessionCache.Remove(userId.ToString());
        }

        //Custom Method
        public static async Task StartSession(User user, IUserService _userService)
        {
            var roles = await _userService.GetUserRolesAsync(user.Id);
            var permissions = await _userService.GetUserPermissionsAsync(user.Id);
            LoggedUserId = user.Id;
            SetSessionData(user.Id, user.Name, roles, permissions, user.IsSupplier);
        }
        public static Task<bool> IsInRoleAsync(string roleName)
        {
            return Task.Run(() =>
            {
                SessionData sessionData = SessionCache.Get(LoggedUserId.ToString()) as SessionData ?? new SessionData();
                var roles = sessionData.Roles.ToList();
                return roles.Contains(roleName);
            });
        }
        public static Task<bool> HasPermissionAsync(string permissionName)
        {
            return Task.Run(() => {
                SessionData sessionData = SessionCache.Get(LoggedUserId.ToString()) as SessionData ?? new SessionData();
                var permissions = sessionData.Permissions.ToList();
                return permissions.Contains(permissionName);
            });
        }
        public static void Logout()
        {
            ClearSessionData(LoggedUserId);
        }
        public static bool IsLoggedIn()
        {
            var sessionData = GetSessionData(LoggedUserId);
            return sessionData.UserId > 0 ? true : false;
        }
        public static string GetUserName()
        {
            return GetSessionData(LoggedUserId).UserName;
        }
    }

    public class SessionData
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> Permissions { get; set; } = Enumerable.Empty<string>();
        public bool IsSupplierAccount { get; set; }
    }
}
