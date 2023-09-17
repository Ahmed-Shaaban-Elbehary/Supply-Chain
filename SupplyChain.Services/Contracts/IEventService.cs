using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> GetAllPagedEventsAsync(int page, int pageSize);
        Task<Event> GetEventByIdAsync(int id);
        Task<IEnumerable<Event>> GetIntervalEvent(DateTime start, DateTime end);
        Task<int> CreateEventAsync(Event _event);
        Task<int> UpdateEventAsync(Event _event);
        Task<int> DeleteEventAsync(Event _event);
        Task<int> CountEventAsync();
        Task RollbackTransaction();
    }
}
