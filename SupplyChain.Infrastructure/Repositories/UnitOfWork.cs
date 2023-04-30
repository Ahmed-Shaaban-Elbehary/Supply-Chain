using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SupplyChainDbContext _context;
        private readonly IDbContextTransaction _transaction;
        public UnitOfWork(SupplyChainDbContext context)
        {
            _context = context;
            ProductRepository = new GenericRepository<Product>(_context);
            CartRepository = new GenericRepository<Cart>(_context);
            _transaction = _context.Database.BeginTransaction();
        }

        public IGenericRepository<Product> ProductRepository { get; private set; }

        public IGenericRepository<Cart> CartRepository { get; private set; }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
