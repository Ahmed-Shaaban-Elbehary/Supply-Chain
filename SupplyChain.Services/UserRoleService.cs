using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddMultipleUserRoleAsync(int userId, List<int> roleIds)
        {
            foreach (var roleId in roleIds)
            {
                UserRole ur = new UserRole();
                ur.UserId = userId;
                ur.RoleId = roleId;
                await _unitOfWork.UserRoleRepository.AddAsync(ur);
            }
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> AddSingleUserRoleAsync(int userId, int roleId)
        {
            UserRole ur = new UserRole() { RoleId = roleId, UserId = userId };
            await _unitOfWork.UserRoleRepository.AddAsync(ur);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> CountUserRolesAsync()
        {
            return await _unitOfWork.UserRepository.CountAsync();
        }

        public async Task<int> DeleteUserRoleAsync(int userId, int roleId)
        {
            UserRole ur = new UserRole() { RoleId = roleId, UserId = userId };
            await _unitOfWork.UserRoleRepository.RemoveAsync(ur);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAllPagedUserRoleAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.UserRoleRepository
                .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.UserId), true);
            return result;
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRoleAsync()
        {
            return await _unitOfWork.UserRoleRepository.GetAllAsync();
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesByRoleIdAsync(int roleId)
        {
            return await _unitOfWork.UserRoleRepository.GetWhereAsync(ur => ur.RoleId == roleId);
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesByUserIdAsync(int userId)
        {
            return await _unitOfWork.UserRoleRepository.GetWhereAsync(ur => ur.UserId == userId);
        }

        public async Task<int> UpdateMultipleUserRolesAsync(int userId, List<int> roleIds)
        {
            // Remove the existing user roles for the user.
            var existingUserRoles = await _unitOfWork.UserRoleRepository.GetWhereAsync(ur => ur.UserId == userId);
            await _unitOfWork.UserRoleRepository.RemoveRangeAsync(existingUserRoles);

            // Add the new user roles for the user.
            var addedRolesCount = await AddMultipleUserRoleAsync(userId, roleIds);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> UpdateSingleUserRolesAsync(int userId, int roleId)
        {
            // Remove the existing user roles for the user.
            var existingUserRoles = await _unitOfWork.UserRoleRepository.GetWhereAsync(ur => ur.UserId == userId);
            await _unitOfWork.UserRoleRepository.RemoveRangeAsync(existingUserRoles);

            // Add the new user roles for the user.
            var addedRolesCount = await AddSingleUserRoleAsync(userId, roleId);
            return await _unitOfWork.CommitAsync();
        }
    }
}
