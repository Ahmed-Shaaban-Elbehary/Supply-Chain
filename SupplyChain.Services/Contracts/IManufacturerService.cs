using SupplyChain.Core.Models;

namespace SupplyChain.Services.Contracts
{
    public interface IManufacturerService
    {
        Task<IEnumerable<Manufacturer>> GetAllManufacturerAsync();
        Task<IEnumerable<Manufacturer>> GetAllPagedManufacturerAsync(int page, int pageSize);
        Task<Manufacturer> GetManufacturerByIdAsync(int id);
        Task CreateManufacturerAsync(Manufacturer manufacturer);
        Task UpdateManufacturerAsync(Manufacturer manufacturer);
        Task DeleteManufacturerAsync(Manufacturer manufacturer);
        Task<int> CountManufacturerAsync();
        Task RollbackTransaction();
    }
}
