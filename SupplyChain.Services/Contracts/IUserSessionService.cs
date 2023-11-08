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
        bool IsUserSupplier {  get; }
        int GetUserId();
        string GetUserName();
        bool HasPermission(string permissionName);
        bool IsInRole(string roleName);
        void ClearUserSession();
    }
}
