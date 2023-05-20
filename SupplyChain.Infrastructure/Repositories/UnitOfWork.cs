using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SupplyChainDbContext _context;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<Manufacturer> _manufactureRepository;
        private readonly IGenericRepository<Cart> _cartRepository;

        public UnitOfWork(SupplyChainDbContext context)
        {
            _context = context;
            _productRepository = new GenericRepository<Product>(_context);
            _productCategoryRepository = new GenericRepository<ProductCategory>(_context);
            _cartRepository = new GenericRepository<Cart>(_context);
            _manufactureRepository = new GenericRepository<Manufacturer>(_context);
        }

        public IGenericRepository<Product> ProductRepository => _productRepository;
        public IGenericRepository<ProductCategory> ProductCategoryRepository => _productCategoryRepository;
        public IGenericRepository<Manufacturer> ManufactureRepository => _manufactureRepository;
        public IGenericRepository<Cart> CartRepository => _cartRepository;

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
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
