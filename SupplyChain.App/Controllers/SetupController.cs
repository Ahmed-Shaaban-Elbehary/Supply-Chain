using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyChain.App.Mappers.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

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

        [HttpGet]
        public async Task<ActionResult> Category()
        {
            var viewModel = new ProductCategoryViewModel();
            ViewBag.Categories = await _productCategoryService.GetAllProductCategoriesAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(ProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = _productCategoryMapper.MapViewModelToProductCategory(vm);
                await _productCategoryService.CreateProductCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }

            // If the model is not valid, redisplay the form with validation errors
            ViewBag.Categories = await _productCategoryService.GetAllProductCategoriesAsync();
            return View(vm);
        }

        public IActionResult Manufacturer()
        {
            return View();
        }


    }
}
