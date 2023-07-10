using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SupplyChainDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly INotifcationRepository _notifcationRepository;

        public UnitOfWork(SupplyChainDbContext context)
        {
            _context = context;
            _productRepository = new ProductRepository(_context);
            _productCategoryRepository = new ProductCategoryRepository(_context);
            _cartRepository = new CartRepository(_context);
            _manufacturerRepository = new ManufacturerRepository(_context);
            _userRepository = new UserRepository(_context);
            _permissionRepository = new PermissionRepository(_context);
            _roleRepository = new RoleRepository(_context);
            _rolePermissionRepository = new RolePermissionRepository(_context);
            _userRoleRepository = new UserRoleRepository(_context);
            _notifcationRepository = new NotificationRepository(_context);
        }

        public IProductRepository ProductRepository => _productRepository;
        public IProductCategoryRepository ProductCategoryRepository => _productCategoryRepository;
        public IManufacturerRepository ManufacturerRepository => _manufacturerRepository;
        public ICartRepository CartRepository => _cartRepository;
        public IUserRepository UserRepository => _userRepository;
        public IPermissionRepository PermissionRepository => _permissionRepository;
        public IRoleRepository RoleRepository => _roleRepository;
        public IRolePermissionRepository RolePermissionRepository => _rolePermissionRepository;
        public IUserRoleRepository UserRoleRepository => _userRoleRepository;
        public INotifcationRepository NotifcationRepository => _notifcationRepository;

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        await entry.ReloadAsync();
                        break;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
