using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class SetupController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IMapper _mapper;
        public SetupController(IProductCategoryService productCategoryService,
            IManufacturerService manufacturerService,
            IMapper mapper)
        {
            _productCategoryService = productCategoryService;
            _manufacturerService = manufacturerService;
            _mapper = mapper;
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
            var vm = _mapper.Map<List<ProductCategoryViewModel>>(categories);

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
                var category = await _productCategoryService.GetProductCategoryByIdAsync(id);
                vm = _mapper.Map<ProductCategoryViewModel>(category);
            }

            return PartialView("~/Views/Setup/PartialViews/_AddEditCategoryForm.cshtml", vm);
        }

        [HttpPost]
        public async Task<ActionResult> AddEditCategory(ProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<ProductCategory>(vm);

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
        public async Task<IActionResult> Manufacturer(int page = 1, int pageSize = 10)
        {
            var manufacturers = await _manufacturerService.GetAllPagedManufacturerAsync(page, pageSize);
            var vm = _mapper.Map<List<ManufacturerViewModel>>(manufacturers);

            var pagedModel = new PagedViewModel<ManufacturerViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _productCategoryService.CountProductCategoryAsync()
            };

            return View(pagedModel);
        }
    }
}
