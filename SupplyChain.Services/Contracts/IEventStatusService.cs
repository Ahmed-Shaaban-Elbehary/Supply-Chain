using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IEventStatusService
    {
        Task<IEnumerable<EventStatus>> GetAllEventStatusAsync();
        Task<IEnumerable<EventStatus>> GetAllPagedEventStatusAsync(int page, int pageSize);
        Task<EventStatus> GetEventStatusByIdAsync(int id);
        EventStatus GetEventStatusByEventIdAndUserIdAsync(int eventId, int userId);
        Task CreateEventStatusAsync(EventStatus eventStatus);
        Task UpdateEventStatusAsync(EventStatus eventStatus);
        Task DeleteEventStatusAsync(EventStatus eventStatus);
        Task<int> CountEventStatusAsync();
        Task RollbackTransaction();
    }
}
