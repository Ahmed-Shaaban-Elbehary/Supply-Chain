using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace SupplyChain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<int> CountUserAsync()
        {
            return await _unitOfWork.UserRepository.CountAsync();
        }

        public async Task<int> CreateUserAsync(User user, string password)
        {
            user.Password = ComputeMD5Hash(password);
            await _userRepository.AddUserAsync(user);
            var result = await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
            return result;
        }

        public async Task<int> DeleteUserAsync(User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                await _unitOfWork.UserRepository.RemoveAsync(user);
                var userRole = await _unitOfWork.UserRoleRepository.GetUserRoleByUserIdAsync(user.Id);
                if (userRole != null)
                {
                    await _unitOfWork.UserRoleRepository.RemoveAsync(userRole);
                }
                var result = await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransaction();
                return result;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
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
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            user.UserRoles = await _unitOfWork.UserRoleRepository.GetUserRolesByUserIdAsync(userId);
            return user;
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

        public async Task<int> UpdateUserAsync(User user, string newPassword, int roleId, bool isPasswordChanged)
        {
            try
            {
                var userInDb = GetUserByIdAsync(user.Id).Result;
                if (isPasswordChanged)
                {
                    user.Password = isPasswordChanged
                        ? user.Password = newPassword
                        : user.Password = userInDb.Password;
                    user.Password = ComputeMD5Hash(user.Password);
                }
                else
                {
                    user.Password = userInDb.Password;
                }
                // If the user is already in the context, detach it
                await _unitOfWork.Detach(userInDb);
                await _unitOfWork.BeginTransaction();
                await _unitOfWork.UserRepository.UpdateAsync(user);
                var userRole = await _unitOfWork.UserRoleRepository.GetUserRoleByUserIdAsync(user.Id);
                if (userRole != null)
                {
                    await _unitOfWork.UserRoleRepository.UpdateAsync(userRole);
                }
                else
                {
                    UserRole ur = new UserRole() { RoleId = roleId, UserId = user.Id };
                    await _unitOfWork.UserRoleRepository.AddAsync(ur);
                }
                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransaction();
                return 1;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetSingleAsync(u => u.Email == email);
                if (user == null)
                {
                    return false;
                }

                var result = VerifyPassword(user.Password, password);
                return result;
            }
            catch
            {
                return false;
            }

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
