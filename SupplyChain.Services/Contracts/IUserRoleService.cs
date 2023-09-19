﻿using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllUserRoleAsync();
        Task<IEnumerable<UserRole>> GetAllPagedUserRoleAsync(int page, int pageSize);
        Task<IEnumerable<UserRole>> GetUserRolesByUserIdAsync(int userId);
        Task<IEnumerable<UserRole>> GetUserRolesByRoleIdAsync(int roleId);
        Task<List<int>> AddUserRolesAsync(User user, List<int> roleIds);
        Task<int> DeleteUserRoleAsync(int userId, int roleId);
        public Task<int> DeleteUserRolesAsync(int userId, List<int> roleIds);
        Task<int> UpdateUserRolesAsync(User user, List<int> roleIds);
        Task<int> CountUserRolesAsync();
        Task RollbackTransaction();
    }
}
