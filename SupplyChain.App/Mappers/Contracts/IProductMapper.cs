using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Profiles.Contracts
{
    public interface IProductMapper
    {
        Product MapToProduct(ProductViewModel vm);

        IEnumerable<Product> MapToProduct(IEnumerable<ProductViewModel> vm);

        ProductViewModel MapToProductViewModel(Product product);

        IEnumerable<ProductViewModel> MapToProductViewModel(IEnumerable<Product> product);
    }
}
