using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplyChain.App.Profiles.Contracts;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUploadFile _uploadFile;
        private readonly ILookUp _lookUp;
        private readonly IProductMapper _productMapper;

        public ProductController(
            IProductService productService,
            IUploadFile uploadFile,
            ILookUp lookUp,
            IProductMapper productMapper)
        {
            _productService = productService;
            _uploadFile = uploadFile;
            _lookUp = lookUp;
            _productMapper = productMapper;
        }

        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            var vm = products.Count() > 0 ? _productMapper.MapToProductViewModel(products) : new List<ProductViewModel>();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var vm = new ProductViewModel();
            vm.CountryOfOriginList = new SelectList(_lookUp.Countries, "Code", "Name");
            vm.ManufacturerList = new SelectList(_lookUp.Manufacturers, "Id", "Name");
            vm.CategoryList = new SelectList(_lookUp.Categories, "Id", "Name");
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
            var product = new Product();
            product.ImageUrl = await _uploadFile.UploadImage(file);
            product.Description.Trim();

            await _productService.CreateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
