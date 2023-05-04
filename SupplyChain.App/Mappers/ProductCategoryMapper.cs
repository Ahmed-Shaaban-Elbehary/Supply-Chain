using SupplyChain.App.Mappers.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Mappers
{
    public class ProductCategoryMapper : IProductCategoryMapper
    {
        public ProductCategory MapViewModelToProductCategory(ProductCategoryViewModel viewModel)
        {
            return new ProductCategory
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
            };
        }

        public IEnumerable<ProductCategory> MapViewModelToProductCategory(IEnumerable<ProductCategoryViewModel> viewModels)
        {
            var categories = new List<ProductCategory>();
            foreach (var viewModel in viewModels)
            {
                categories.Add(new ProductCategory
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                });
            }
            return categories;
        }

        public ProductCategoryViewModel MapProductCategoryToViewModel(ProductCategory category)
        {
            return new ProductCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public IEnumerable<ProductCategoryViewModel> MapProductCategoryToViewModel(IEnumerable<ProductCategory> categories)
        {
            var viewModels = new List<ProductCategoryViewModel>();
            foreach (var category in categories)
            {
                viewModels.Add(new ProductCategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                });
            }
            return viewModels;
        }
    }
}
