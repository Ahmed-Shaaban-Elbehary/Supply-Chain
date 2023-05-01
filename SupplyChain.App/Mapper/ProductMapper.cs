using SupplyChain.App.Mapper.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Mapper
{
    public class ProductMapper : IProductMapper
    {
        public Product MapToModel(ProductViewModel productViewModel)
        {
            return new Product
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                ImageUrl = productViewModel.ImageUrl,
            };
        }

        public IEnumerable<Product> MapToModel(IEnumerable<ProductViewModel> productViewModels)
        {
            var products = new List<Product>();
            foreach (var item in productViewModels)
            {
                products.Add(MapToModel(item));
            }
            return products;
        }

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
