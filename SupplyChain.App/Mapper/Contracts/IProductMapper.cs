using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Mapper.Contracts
{
    public interface IProductMapper
    {
        ProductViewModel MapToViewModel(Product product);

        IEnumerable<ProductViewModel> MapToViewModel(IEnumerable<Product> products);

        Product MapToModel(ProductViewModel productViewModel);

        IEnumerable<Product> MapToModel(IEnumerable<ProductViewModel> productViewModels);
    }
}
