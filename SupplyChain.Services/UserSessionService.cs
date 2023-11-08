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
        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsUserSupplier => IsSupplier();

        private List<string> GetUserRoles()
        {
            var userRoles = _httpContextAccessor.HttpContext.Request.Cookies["UserRoles"];

            if (string.IsNullOrEmpty(userRoles))
            {
                return new List<string>();
            }

            return userRoles.Split(',').ToList();
        }

        private List<string> GetUserPermissions()
        {
            var userPermissions = _httpContextAccessor.HttpContext.Request.Cookies["UserPermissions"];

            if (string.IsNullOrEmpty(userPermissions))
            {
                return new List<string>();
            }

            return userPermissions.Split(',').ToList();
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            return int.Parse(userId);
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
        }

        public void ClearUserSession()
        {
            // Clear the session data when the user logs out or the session expires
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserSessionToken");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserId");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserRoles");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserPermissions");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserName");
        }

        public bool HasPermission(string permissionName)
        {
            var permissions = GetUserPermissions();
            return permissions.Contains(permissionName);
        }

        public bool IsInRole(string roleName)
        {
            var roles = GetUserRoles();
            return roles.Contains(roleName);
        }

        private bool IsSupplier()
        {
            string supplierCookie = _httpContextAccessor.HttpContext.Request.Cookies["Supplier"];

            // Check if the "Supplier" cookie exists and has a valid boolean value
            if (!string.IsNullOrEmpty(supplierCookie) && bool.TryParse(supplierCookie, out bool isSupplier))
            {
                return isSupplier;
            }

            // If the cookie doesn't exist or can't be parsed, use the default value from IsUserSupplier
            return IsUserSupplier;
        }
    }
}
