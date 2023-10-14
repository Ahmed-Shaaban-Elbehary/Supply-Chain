using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Core.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<int> AddEventAsync(Event _event);
        Task<IEnumerable<Event>> ExecSqlQuery(string sql, params object[] parameters);
    }
}
