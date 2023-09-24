using SupplyChain.App.ViewModels;
using static SupplyChain.App.Utils.Lookups;

namespace SupplyChain.App.Utils.Contracts
{
    public interface ILookUp
    {
        List<Country> Countries { get; }

        List<Unit> Units { get; }

        List<ManufacturerViewModel> Manufacturers { get; }

        List<ProductCategoryViewModel> Categories { get; }

        List<Country> GetCountries();

        List<Unit> GetUnits();

        Task<IEnumerable<ManufacturerViewModel>> GetManufacturers();

        Task<IEnumerable<ProductCategoryViewModel>> GetCategories();
    }
}
