﻿using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace SupplyChain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountUserAsync()
        {
            return await _unitOfWork.UserRepository.CountAsync();
        }

        public async Task<int> CreateUserAsync(User user, string password)
        {
            user.Password = ComputeMD5Hash(password);
            await _unitOfWork.UserRepository.AddAsync(user);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteUserAsync(User User)
        {
            await _unitOfWork.UserRepository.RemoveAsync(User);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> GetAllPagedUsersAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.UserRepository
                .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.UserRepository.GetSingleAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<string>> GetUserPermissionsAsync(int userId)
        {
            return await _unitOfWork.UserRepository.GetUserPermissionsAsync(userId);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        {
            var userRoles = await _unitOfWork.UserRoleRepository.GetUserRolesByUserIdAsync(userId);
            var roleIds = userRoles.Select(ur => ur.RoleId).ToList();
            var roles = await _unitOfWork.RoleRepository.GetWhereAsync(r => roleIds.Contains(r.Id));
            return roles.Select(r => r.Name).ToList();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId)
        {
            var userRoles = await _unitOfWork.UserRoleRepository.GetUserRolesByRoleIdAsync(roleId);
            var userIds = userRoles.Select(ur => ur.UserId).ToList();
            return (await _unitOfWork.UserRepository.GetWhereAsync(u => userIds.Contains(u.Id))).ToList();
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            await _unitOfWork.UserRepository.UpdateAsync(user);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            var user = await _unitOfWork.UserRepository.GetSingleAsync(u => u.Email == email);
            if (user == null)
            {
                return false;
            }

            var result = VerifyPassword(user.Password, password);
            return result;
        }

        public string ComputeMD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);
                var hash = new StringBuilder();

                foreach (var b in hashBytes)
                {
                    hash.Append(b.ToString("x2"));
                }

                return hash.ToString();
            }
        }

        public bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            var inputPasswordHash = ComputeMD5Hash(inputPassword);
            return hashedPassword.Equals(inputPasswordHash);
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }
    }
}
