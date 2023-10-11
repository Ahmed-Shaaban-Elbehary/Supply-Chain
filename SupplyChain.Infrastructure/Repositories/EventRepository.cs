using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Infrastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly SupplyChainDbContext _context;
        public EventRepository(SupplyChainDbContext dbContext) : base(dbContext) { _context = dbContext; }

        public async Task<int> AddEventAsync(Event _event)
        {
            await _context.Events.AddAsync(_event);
            await _context.SaveChangesAsync();
            var property = _event.GetType().GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException("The entity does not have an 'Id' property.");

            return (int)(property?.GetValue(_event) ?? 0);
        }

        public async Task<IEnumerable<Event>> ExecSqlQuery(string sql, params object[] parameters)
        {
            return await _context.Events.FromSqlRaw(sql, parameters).ToListAsync();
        }
    }
}
