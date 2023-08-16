using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SupplyChain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SupplyChain.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SupplyChainDbContext _context;
        private readonly DbSet<T> _set;

        public GenericRepository(SupplyChainDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _set.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate != null ? _set.AnyAsync(predicate) : _set.AnyAsync());
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate != null ? _set.CountAsync(predicate) : _set.CountAsync());
        }

        public async Task ExecuteSqlCommand(string sql, params object[] parameters)
        {
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _set.AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _set.FindAsync(id);
            if (result == null)
                throw new InvalidOperationException("No matching element was found.");

            return result;
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _set.FirstOrDefaultAsync(predicate);
            if (result == null)
                throw new InvalidOperationException("No matching element was found.");

            return result;
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, Func<T, object> orderSelector, bool ascendingOrder = true)
        {
            var query = _set.AsQueryable().Where(predicate);

            if (ascendingOrder)
            {
                query = query.OrderBy(orderSelector).AsQueryable();
            }
            else
            {
                query = query.OrderByDescending(orderSelector).AsQueryable();
            }

            var result = await query.FirstOrDefaultAsync();

            if (result == null)
                throw new InvalidOperationException("No matching element was found.");

            return result;
        }

        public async Task<IEnumerable<T>?> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool ascendingOrder = true)
        {
            var query = _set.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            if (result.Count == 0)
            {
                return null;
            }
            return result;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _set.AsQueryable().Where(predicate);

            var result = await query.SingleOrDefaultAsync();

            if (result == null)
                throw new InvalidOperationException("No matching element was found.");

            return result;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, Func<T, object> orderSelector, bool ascendingOrder = true)
        {
            var query = _set.AsQueryable().Where(predicate);

            if (ascendingOrder)
            {
                query = query.OrderBy(orderSelector).AsQueryable();
            }
            else
            {
                query = query.OrderByDescending(orderSelector).AsQueryable();
            }

            var result = await query.SingleOrDefaultAsync();

            if (result == null)
                throw new InvalidOperationException("No matching element was found.");

            return result;
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetWhereQueryable(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(predicate);
        }

        public async Task RemoveAsync(T entity)
        {
            _set.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _set.RemoveRange(entities);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(T entity)
        {
            _set.Update(entity);
            await Task.CompletedTask;
        }
    }
}
