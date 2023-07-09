using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface ICurrentUserSerivce
    {
        int UserId { get; set; }
        string UserName { get; set; }

        Task SetUserAsync(User user);
        bool IsInRole(string roleName);
        bool HasPermission(string permissionName);
    }
}
