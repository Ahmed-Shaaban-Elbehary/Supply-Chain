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
        int GetUserId();
        string GetUserName();
        bool HasPermissionAsync(string permissionName);
        bool IsInRoleAsync(string roleName);
        void ClearUserSession();
    }
}
