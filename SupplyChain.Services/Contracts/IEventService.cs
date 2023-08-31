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
        Task<IEnumerable<Event>> GetAllManufacturerAsync();
        Task<IEnumerable<Event>> GetAllPagedManufacturerAsync(int page, int pageSize);
        Task<Event> GetManufacturerByIdAsync(int id);
        Task CreateManufacturerAsync(Event _event);
        Task UpdateManufacturerAsync(Event _event);
        Task DeleteManufacturerAsync(Event _event);
        Task<int> CountManufacturerAsync();
        Task RollbackTransaction();
    }
}
