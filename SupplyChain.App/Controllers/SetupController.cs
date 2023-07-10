using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.App.Utils.Validations;
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
            IMapper mapper,
            IManufacturerService manufacturerService)
        {
            _productCategoryService = productCategoryService;
            _mapper = mapper;
            _manufacturerService = manufacturerService;
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
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddEditCategory(ProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<ProductCategory>(vm);

                if (category.Id == 0) // Adding a new category
                {
                    await _productCategoryService.CreateProductCategoryAsync(category);
                    return Json(new { message = "Add New Category Successed!" });
                }
                else // Editing an existing category
                {
                    await _productCategoryService.UpdateProductCategoryAsync(category);
                    return Json(new { message = "Edit Category Successed!" });
                }

            }

            // If the model is not valid, redisplay the form with validation errors
            //ViewBag.Categories = await _productCategoryService.GetAllProductCategoriesAsync();
            return Json(new { message = "Oops, Error Occurred, Please Try Again!" });
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

        #region Manufacturer
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
                TotalItems = await _manufacturerService.CountManufacturerAsync()
            };

            return View(pagedModel);
        }

        [HttpGet]
        public async Task<ActionResult> AddEditManufacturer(int id)
        {
            var vm = new ManufacturerViewModel();

            if (id > 0)
            {
                var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
                vm = _mapper.Map<ManufacturerViewModel>(manufacturer);
            }

            return PartialView("~/Views/Setup/PartialViews/_AddEditManufacturerForm.cshtml", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddEditManufacturer(ManufacturerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var manufacturer = _mapper.Map<Manufacturer>(vm);

                if (manufacturer.Id == 0) // Adding a new category
                {
                    await _manufacturerService.CreateManufacturerAsync(manufacturer);
                    return Json(new { message = "Add New Manufacturer Successed!" });
                }
                else // Editing an existing category
                {
                    await _manufacturerService.UpdateManufacturerAsync(manufacturer);
                    return Json(new { message = "Edit Manufacturer Successed!" });
                }

            }

            // If the model is not valid, redisplay the form with validation errors
            //ViewBag.Manufacturers = await _manufacturerService.GetAllManufacturerAsync();
            return Json(new { message = "Oops, Error Occurred, Please Try Again!" });
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteManufacturer(int id)
        {
            if (id > 0)
            {
                var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
                await _manufacturerService.DeleteManufacturerAsync(manufacturer);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion Manufacturer

        #region User Roles Management System
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult Permissions()
        {
            return View();
        }
        public IActionResult RolePermissions()
        {
            return View();
        }
        public IActionResult UserRoles()
        {
            return View();
        }
        #endregion User Roles Management System 
    }
}
