using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplyChain.App.App_Class;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly DependencyContainer container;
        public ProductController(
            DependencyContainer _container)
        {
            _container = container;
        }

        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var products = await container._productService.GetAllProductsAsync();
            var vm = products.Count() > 0 ? container._mapper.Map<List<ProductViewModel>>(products) : new List<ProductViewModel>();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var vm = new ProductViewModel();
            vm.CountryOfOriginList = new SelectList(container._lookup.Countries, "Code", "Name");
            vm.ManufacturerList = new SelectList(container._lookup.Manufacturers, "Id", "Name");
            vm.CategoryList = new SelectList(container._lookup.Categories, "Id", "Name");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductViewModel vm, IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file == null || file.Length == 0 || !allowedExtensions.Contains(extension))
            {
                return BadRequest("Invalid file type.");
            }
            vm.ImageUrl = await container._uploadFile.UploadImage(file);
            vm.Description.Trim();
            vm.Name.Trim();
            await container._productService.CreateProductAsync(container._mapper.Map<Product>(vm));
            return RedirectToAction(nameof(Index));
        }
    }
}
