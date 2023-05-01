using SupplyChain.App.Mapper.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Mapper
{
    public class ProductMapper : IProductMapper
    {
        public ProductViewModel MapToViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
            };
        }
        public IEnumerable<ProductViewModel> MapToViewModel(IEnumerable<Product> product)
        {
            var ProductViewModel = new List<ProductViewModel>();
            foreach (var item in product)
            {
                ProductViewModel.Add(MapToViewModel(item));
            }
            return ProductViewModel;
        }
    }
}
