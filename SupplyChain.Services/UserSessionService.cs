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
        private const string UserSessionKey = "CurrentUser";
        private User? currentUser;
        private List<string> userPermissions = new List<string>();
        private List<string> userRoles = new List<string>();

        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> GetUserAsync()
        {
            byte[] userData;
            if (_httpContextAccessor.HttpContext.Session.TryGetValue(UserSessionKey, out userData))
            {
                // Deserialize the user data from the session
                var userJson = Encoding.UTF8.GetString(userData);
                var result = Task.Run(() =>
                {
                    return JsonConvert.DeserializeObject<User>(userJson);
                });
                return await result;
            }

            return null;
        }

        public Task<IEnumerable<string>> GetUserPermissionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUserRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPermissionAsync(string permission)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserLoggedInAsync()
        {
            throw new NotImplementedException();
        }

        public Task LoginAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task SetUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
