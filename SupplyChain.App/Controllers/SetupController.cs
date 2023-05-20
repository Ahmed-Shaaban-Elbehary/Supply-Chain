using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupplyChain.App.Mappers.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Services.Contracts;
using System.Drawing.Printing;

namespace SupplyChain.App.Controllers
{
    public class SetupController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductCategoryMapper _productCategoryMapper;
        public SetupController(IProductCategoryService productCategoryService,
            IProductCategoryMapper productCategoryMapper)
        {
            _productCategoryService = productCategoryService;
            _productCategoryMapper = productCategoryMapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Category
        [HttpGet]
        public async Task<ActionResult> Category(int page = 1, int pageSize = 10)
        {
            var categories = await _productCategoryService.GetAllPagedProductCategoriesAsync(page, pageSize);
            var vm = _productCategoryMapper.MapProductCategoryToViewModel(categories);

            var pagedModel = new PagedViewModel<ProductCategoryViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _productCategoryService.CountProductCategoryAsync()
            };

            return View(pagedModel);
        }

        [HttpGet]
        public async Task<ActionResult> AddEditCategory(int id)
        {
            var vm = new ProductCategoryViewModel();

            if (id > 0)
            {
                var productCategory = await _productCategoryService.GetProductCategoryByIdAsync(id);
                vm = _productCategoryMapper.MapProductCategoryToViewModel(productCategory);
            }

            return PartialView("~/Views/Setup/PartialViews/_AddEditCategoryForm.cshtml", vm);
        }

        [HttpPost]
        public async Task<ActionResult> AddEditCategory(ProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = _productCategoryMapper.MapViewModelToProductCategory(vm);

                if (category.Id == 0) // Adding a new category
                {
                    await _productCategoryService.CreateProductCategoryAsync(category);
                }
                else // Editing an existing category
                {
                    await _productCategoryService.UpdateProductCategoryAsync(category);
                }

                return RedirectToAction(nameof(Category));
            }

            // If the model is not valid, redisplay the form with validation errors
            ViewBag.Categories = await _productCategoryService.GetAllProductCategoriesAsync();
            return View(vm);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteCategory(int id)
        {
            if (id > 0)
            {
                var productCategory = await _productCategoryService.GetProductCategoryByIdAsync(id);
                await _productCategoryService.DeleteProductCategoryAsync(productCategory);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion Category


        [HttpGet]
        public IActionResult Manufacturer()
        {
            return View();
        }
    }
}
