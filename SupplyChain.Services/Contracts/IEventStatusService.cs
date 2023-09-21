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
        Task<IEnumerable<EventStatus>> GetAllNotificationAsync();
        Task<IEnumerable<EventStatus>> GetAllPagedNotificationAsync(int page, int pageSize);
        Task<EventStatus> GetNotificationByIdAsync(int id);
        Task CreateNotificationAsync(EventStatus notification);
        Task UpdateNotificationAsync(EventStatus notification);
        Task DeleteNotificationAsync(EventStatus notification);
        Task<int> CountNotificationAsync();
        Task RollbackTransaction();
    }
}
