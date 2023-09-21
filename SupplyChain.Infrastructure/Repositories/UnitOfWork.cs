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
        private readonly IEventStatusRepository _notifcationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IProductEventRepository _productEventRepository;
        private IDbContextTransaction _transaction;
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
            _notifcationRepository = new EventStatusRepository(_context);
            _eventRepository = new EventRepository(_context);
            _productEventRepository = new ProductEventRepository(_context);
            _transaction = _context.Database.BeginTransactionAsync().Result;
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
        public IEventStatusRepository NotifcationRepository => _notifcationRepository;
        public IEventRepository EventRepository => _eventRepository;
        public IProductEventRepository ProductEventRepository => _productEventRepository;

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

        public async Task Detach<T>(T entity) where T : class
        {
            var entry = _context.Entry(entity);

            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }

            await Task.CompletedTask;
        }

        public async Task BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransaction()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }
    }
}
