using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Mappers.Contracts
{
    public interface IProductCategoryMapper
    {
        ProductCategory MapViewModelToProductCategory(ProductCategoryViewModel vm);

        IEnumerable<ProductCategory> MapViewModelToProductCategory(IEnumerable<ProductCategoryViewModel> vm);

        ProductCategoryViewModel MapProductCategoryToViewModel(ProductCategory productCategory);

        IEnumerable<ProductCategoryViewModel> MapProductCategoryToViewModel(IEnumerable<ProductCategory> productCategories);
    }
}
