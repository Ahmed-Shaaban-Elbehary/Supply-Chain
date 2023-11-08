using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System.Text;

namespace SupplyChain.Services
{
    public class UserSessionService : IUserSessionService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<string>> GetUserRolesAsync()
        {
            var userRoles = _httpContextAccessor.HttpContext.Request.Cookies["UserRoles"];

            if (string.IsNullOrEmpty(userRoles))
            {
                return new List<string>();
            }

            return userRoles.Split(',').ToList();
        }

        public async Task<List<string>> GetUserPermissionsAsync()
        {
            var userPermissions = _httpContextAccessor.HttpContext.Request.Cookies["UserPermissions"];

            if (string.IsNullOrEmpty(userPermissions))
            {
                return new List<string>();
            }

            return userPermissions.Split(',').ToList();
        }

        public async Task<string> GetUserIdAsync()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
        }

        public async Task<string> GetUserNameAsync()
        {
            return await Task.FromResult( _httpContextAccessor.HttpContext.Request.Cookies["UserName"]);
        }

        public async Task ClearUserSessionAsync()
        {
            // Clear the session data when the user logs out or the session expires
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserSessionToken");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserId");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserRoles");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserPermissions");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserName");
        }
    }
}
