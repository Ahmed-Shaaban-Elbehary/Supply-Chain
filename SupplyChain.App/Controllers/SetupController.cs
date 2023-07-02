using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.App.App_Class;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class SetupController : Controller
    {
        private readonly DependencyContainer container;
        public SetupController(DependencyContainer dependencyContainer)
        {
            container = dependencyContainer;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Category
        [HttpGet]
        public async Task<ActionResult> Category(int page = 1, int pageSize = 10)
        {
            var categories = await container._productCategoryService.GetAllPagedProductCategoriesAsync(page, pageSize);
            var vm = container._mapper.Map<List<ProductCategoryViewModel>>(categories);

            var pagedModel = new PagedViewModel<ProductCategoryViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await container._productCategoryService.CountProductCategoryAsync()
            };

            return View(pagedModel);
        }

        [HttpGet]
        public async Task<ActionResult> AddEditCategory(int id)
        {
            var vm = new ProductCategoryViewModel();

            if (id > 0)
            {
                var category = await container._productCategoryService.GetProductCategoryByIdAsync(id);
                vm = container._mapper.Map<ProductCategoryViewModel>(category);
            }

            return PartialView("~/Views/Setup/PartialViews/_AddEditCategoryForm.cshtml", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddEditCategory(ProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = container._mapper.Map<ProductCategory>(vm);

                if (category.Id == 0) // Adding a new category
                {
                    await container._productCategoryService.CreateProductCategoryAsync(category);
                    return Json(new { message = "Add New Category Successed!" });
                }
                else // Editing an existing category
                {
                    await container._productCategoryService.UpdateProductCategoryAsync(category);
                    return Json(new { message = "Edit Category Successed!" });
                }

            }

            // If the model is not valid, redisplay the form with validation errors
            ViewBag.Categories = await container._productCategoryService.GetAllProductCategoriesAsync();
            return Json(vm, new { });
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteCategory(int id)
        {
            if (id > 0)
            {
                var productCategory = await container._productCategoryService.GetProductCategoryByIdAsync(id);
                await container._productCategoryService.DeleteProductCategoryAsync(productCategory);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion Category


        [HttpGet]
        public async Task<IActionResult> Manufacturer(int page = 1, int pageSize = 10)
        {
            var manufacturers = await container._manufacturerService.GetAllPagedManufacturerAsync(page, pageSize);
            var vm = container._mapper.Map<List<ManufacturerViewModel>>(manufacturers);

            var pagedModel = new PagedViewModel<ManufacturerViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await container._productCategoryService.CountProductCategoryAsync()
            };

            return View(pagedModel);
        }
    }
}
