using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IProductEventService
    {
        Task<IEnumerable<Event>> GetAllProductEventsAsync();
        Task<IEnumerable<Event>> GetAllPagedProductEventsAsync(int page, int pageSize);
        Task<int> CountProductEventAsync();
        Task<List<int>> AddProductEventAsync(Event _event, List<int> productIds);
        Task<int> UpdateProductEventAsync(Event _event, List<int> productIds);
        Task<IEnumerable<ProductEvent>> GetProductEventByProductIdAsync(int productId);
        Task<IEnumerable<ProductEvent>> GetProductEventByEventIdAsync(int eventId);
    }
}
