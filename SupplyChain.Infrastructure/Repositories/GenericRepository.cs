using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _set;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _set.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetWhereQueryable(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(predicate).AsQueryable();
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>> predicate = null, Func<T, object> orderSelector = null, bool ascendingOrder = true)
        {
            var query = _set.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderSelector != null)
            {
                query = ascendingOrder ? query.OrderBy(orderSelector) : query.OrderByDescending(orderSelector);
            }

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await _set.CountAsync(predicate);
            }

            return await _set.CountAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await _set.AnyAsync(predicate);
            }

            return await _set.AnyAsync();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, Func<T, object> orderSelector, bool ascendingOrder = true)
        {
            var query = _set.Where(predicate);

            if (orderSelector != null)
            {
                query = ascendingOrder ? query.OrderBy(orderSelector) : query.OrderByDescending(orderSelector);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.SingleOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, Func<T, object> orderSelector, bool ascendingOrder = true)
        {
            var query = _set.Where(predicate);

            if (orderSelector != null)
            {
                query = ascendingOrder ? query.OrderBy(orderSelector) : query.OrderByDescending(orderSelector);
            }

            return await query.SingleOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _set.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _set.Update(entity);
        }

        public void Remove(T entity)
        {
            _set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _set.RemoveRange(entities);
        }
    }
}
