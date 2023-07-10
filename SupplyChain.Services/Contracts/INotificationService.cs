using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllManufacturerAsync();
        Task<IEnumerable<Notification>> GetAllPagedManufacturerAsync(int page, int pageSize);
        Task<Notification> GetManufacturerByIdAsync(int id);
        Task CreateManufacturerAsync(Notification notification);
        Task UpdateManufacturerAsync(Notification notification);
        Task DeleteManufacturerAsync(Notification notification);
        Task<int> CountManufacturerAsync();
    }
}
