using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Mapper.Contracts
{
    public interface IProductMapper
    {
        ProductViewModel MapToViewModel(Product result);
        IEnumerable<ProductViewModel> MapToViewModel(IEnumerable<Product> result);
    }
}
