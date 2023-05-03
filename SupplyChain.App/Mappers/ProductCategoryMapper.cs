using SupplyChain.App.Mappers.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Mappers
{
    public class ProductCategoryMapper : IProductCategoryMapper
    {
        public ProductCategory MapViewModelToProductCategory(ProductCategoryViewModel viewModel)
        {
            var category = new ProductCategory
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                ParentCategoryId = viewModel.ParentCategoryId
            };

            // if ParentCategoryName is not null, create a new ParentCategory object and assign it to the ParentCategory property
            if (!string.IsNullOrEmpty(viewModel.ParentCategoryName))
            {
                category.ParentCategory = new ProductCategory
                {
                    Name = viewModel.ParentCategoryName
                };
            }

            // map the subcategories
            category.Subcategories = new List<ProductCategory>();
            foreach (var subcategoryViewModel in viewModel.Subcategories)
            {
                category.Subcategories.Add(MapViewModelToProductCategory(subcategoryViewModel)); // recursively map subcategories
            }

            // map the products
            category.Products = new List<Product>();
            foreach (var productViewModel in viewModel.Products)
            {
                category.Products.Add(new Product
                {
                    Id = productViewModel.Id,
                    Name = productViewModel.Name,
                    Description = productViewModel.Description
                });
            }

            return category;
        }

        public IEnumerable<ProductCategory> MapViewModelToProductCategory(IEnumerable<ProductCategoryViewModel> viewModels)
        {
            var categories = new List<ProductCategory>();
            foreach (var viewModel in viewModels)
            {
                var category = new ProductCategory
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    ParentCategoryId = viewModel.ParentCategoryId
                };

                // if ParentCategoryName is not null, create a new ParentCategory object and assign it to the ParentCategory property
                if (!string.IsNullOrEmpty(viewModel.ParentCategoryName))
                {
                    category.ParentCategory = new ProductCategory
                    {
                        Name = viewModel.ParentCategoryName
                    };
                }

                // map the subcategories
                category.Subcategories = new List<ProductCategory>();
                foreach (var subcategoryViewModel in viewModel.Subcategories)
                {
                    category.Subcategories.Add(MapViewModelToProductCategory(subcategoryViewModel)); // recursively map subcategories
                }

                // map the products
                category.Products = new List<Product>();
                foreach (var productViewModel in viewModel.Products)
                {
                    category.Products.Add(new Product
                    {
                        Id = productViewModel.Id,
                        Name = productViewModel.Name,
                        Description = productViewModel.Description
                    });
                }

                categories.Add(category);
            }

            return categories;
        }

        public ProductCategoryViewModel MapProductCategoryToViewModel(ProductCategory category)
        {
            var viewModel = new ProductCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryName = category.ParentCategory?.Name // if ParentCategory is not null, get its Name property
            };

            // map the subcategories
            viewModel.Subcategories = new List<ProductCategoryViewModel>();
            foreach (var subcategory in category.Subcategories)
            {
                viewModel.Subcategories.Add(MapProductCategoryToViewModel(subcategory)); // recursively map subcategories
            }

            // map the products
            viewModel.Products = new List<ProductViewModel>();
            foreach (var product in category.Products)
            {
                viewModel.Products.Add(new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description
                });
            }

            return viewModel;
        }

        public IEnumerable<ProductCategoryViewModel> MapProductCategoryToViewModel(IEnumerable<ProductCategory> categories)
        {
            var viewModels = new List<ProductCategoryViewModel>();
            foreach (var category in categories)
            {
                var viewModel = new ProductCategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentCategoryId = category.ParentCategoryId,
                    ParentCategoryName = category.ParentCategory?.Name // if ParentCategory is not null, get its Name property
                };

                // map the subcategories
                viewModel.Subcategories = new List<ProductCategoryViewModel>();
                foreach (var subcategory in category.Subcategories)
                {
                    viewModel.Subcategories.Add(MapProductCategoryToViewModel(subcategory)); // recursively map subcategories
                }

                // map the products
                viewModel.Products = new List<ProductViewModel>();
                foreach (var product in category.Products)
                {
                    viewModel.Products.Add(new ProductViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description
                    });
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
