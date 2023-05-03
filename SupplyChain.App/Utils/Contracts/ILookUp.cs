using SupplyChain.App.ViewModels;
using static SupplyChain.App.Utils.Lookups;

namespace SupplyChain.App.Utils.Contracts
{
    public interface ILookUp
    {
        List<Country> Countries { get; }

        List<SelectList> Manufacturers { get; }

        List<ProductCategoryViewModel> Categories { get; }

        List<Country> GetCountries();

        List<SelectList> GetManufacturers();

        Task<IEnumerable<ProductCategoryViewModel>> GetCategories();
    }
}
