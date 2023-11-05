using SupplyChain.App.ViewModels;
using static SupplyChain.App.Utils.Lookups;

namespace SupplyChain.App.Utils.Contracts
{
    public interface ILookUp
    {
        List<SelectItems> Countries { get; }

        List<SelectItems> Units { get; }

        List<ManufacturerViewModel> Manufacturers { get; }

        List<ProductCategoryViewModel> Categories { get; }

        List<SelectItems> GetCountries();

        List<SelectItems> GetUnits();

        Task<IEnumerable<ManufacturerViewModel>> GetManufacturers();

        Task<IEnumerable<ProductCategoryViewModel>> GetCategories();
    }
}
