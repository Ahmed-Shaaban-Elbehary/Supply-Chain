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
        Task<List<string>> GetUserRolesAsync();
        Task<List<string>> GetUserPermissionsAsync();
        Task<string> GetUserIdAsync();
        Task<string> GetUserNameAsync();
        Task ClearUserSessionAsync();
    }
}
