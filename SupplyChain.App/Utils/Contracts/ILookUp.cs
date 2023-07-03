using SupplyChain.App.ViewModels;
using static SupplyChain.App.Utils.Lookups;

namespace SupplyChain.App.Utils.Contracts
{
    public interface ILookUp
    {
        List<Country> Countries { get; }

        List<ManufacturerViewModel> Manufacturers { get; }

        List<ProductCategoryViewModel> Categories { get; }

        List<Country> GetCountries();

        Task<IEnumerable<ManufacturerViewModel>> GetManufacturers();

        Task<IEnumerable<ProductCategoryViewModel>> GetCategories();
    }
}
