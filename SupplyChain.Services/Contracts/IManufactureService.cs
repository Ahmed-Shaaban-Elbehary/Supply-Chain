using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services.Contracts
{
    public interface IManufactureService
    {
        Task<IEnumerable<Manufacturer>> GetAllManufacturerAsync();
        Task<IEnumerable<Manufacturer>> GetAllPagedManufacturerAsync(int page, int pageSize);
        Task<Manufacturer> GetManufacturerByIdAsync(int id);
        Task CreateManufacturerAsync(Manufacturer manufacturer);
        Task UpdateManufacturerAsync(Manufacturer manufacturer);
        Task DeleteManufacturerAsync(Manufacturer manufacturer);
        Task<int> CountManufacturerAsync();
    }
}
