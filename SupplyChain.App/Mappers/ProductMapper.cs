using SupplyChain.App.Profiles.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Profiles
{
    public class ProductMapper : IProductMapper
    {
        public Product MapToProduct(ProductViewModel vm)
        {
            return new Product()
            {
                Id = vm.Id,
                Name = vm.Name,
                ImageUrl = vm.ImageUrl,
                CategoryId = vm.CategoryId,
                CountryOfOriginCode = vm.CountryOfOriginCode,
                ManufacturerId = vm.ManufacturerId,
                Price = vm.Price,
                Quantity = vm.Quantity,
                ExpirationDate = vm.ExpirationDate.Date,
                ProductionDate = vm.ProductionDate.Date,
                Description = vm.Description
            };
        }

        public ProductViewModel MapToProductViewModel(Product product)
        {
            return new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                ProductionDate = product.ProductionDate.Date,
                ExpirationDate = product.ExpirationDate.Date,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                CountryOfOriginCode = product.CountryOfOriginCode,
                ManufacturerId = product.ManufacturerId
            };
        }

        public IEnumerable<Product> MapToProduct(IEnumerable<ProductViewModel> vm)
        {
            List<Product> list = new List<Product>();
            foreach (var item in vm)
            {
                list.Add(MapToProduct(item));
            }
            return list;
        }

        public IEnumerable<ProductViewModel> MapToProductViewModel(IEnumerable<Product> product)
        {
            List<ProductViewModel> list = new List<ProductViewModel>();
            foreach (var item in product)
            {
                list.Add(MapToProductViewModel(item));
            }
            return list;
        }
    }
}
