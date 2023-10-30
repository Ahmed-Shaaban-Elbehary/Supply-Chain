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
        User? GetUser();
        void SetUser(User user);
        bool IsUserLoggedIn();
        bool HasPermission(string permission);
        List<string> GetUserRoles();
        Task<bool> IsInRoleAsync(string roleName);
        void Login(User user);
        void Logout();
    }
}
