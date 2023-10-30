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
        private readonly IUserService _userService;

        public UserSessionService(IHttpContextAccessor httpContextAccessor, 
            IUserService userService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;

            GetUserPermission();
            GetUserRoles();
        }
        private IList<string> UserPersmissions { get; set; } = new List<string>();
        private IList<string> UserRoles { get; set; } = new List<string>();


        public User? GetUser()
        {
            byte[] userBytes;
            if (_httpContextAccessor.HttpContext.Session.TryGetValue("User", out userBytes))
            {
                var userJson = Encoding.UTF8.GetString(userBytes);
                return JsonConvert.DeserializeObject<User>(userJson);
            }
            return null;
        }

        public List<string> GetUserRoles()
        {
            throw new NotImplementedException();
        }

        public bool HasPermission(string permission)
        {
            var user = GetUser();
            if (user != null)
            {
                return UserPersmissions.Contains(permission);
            }
            return false;
        }

        public Task<bool> IsInRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public bool IsUserLoggedIn()
        {
            return GetUser() != null;
        }

        public void Login(User user)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void SetUser(User user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            _httpContextAccessor.HttpContext.Session.Set("User", Encoding.UTF8.GetBytes(userJson));
        }

        private void GetUserPermission()
        {
            var user = GetUser();
            if (user != null)
            {
                this.UserPersmissions = _userService.GetUserPermissionsAsync(user.Id).Result.ToList();
            }
        }
        private void GetUserRoles()
        {
            var user = GetUser();
            if (user != null)
            {
                user.UserRoles = _userService.GetUserRolesAsync(user.Id).Result.ToList();
                this.UserRoles = _userService.Get(user.Id).Result.ToList();
            }
        }
    }
}
