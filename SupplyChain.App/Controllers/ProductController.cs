using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupplyChain.App.Mapper;
using SupplyChain.App.Mapper.Contracts;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductMapper _productMapper;
        private readonly IUploadFile _uploadFile;

        public ProductController(IProductService productService, 
            IProductMapper productMapper, IUploadFile uploadFile)
        {
            _productService = productService;
            _productMapper = productMapper;
            _uploadFile = uploadFile;
        }

        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products == null)
            {
                return NotFound();
            }
            var vm = _productMapper.MapToViewModel(products);
            ViewBag.Countries = Countries.GetCountries();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductViewModel vm, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (file == null || file.Length == 0 || !allowedExtensions.Contains(extension))
            {
                return BadRequest("Invalid file type.");
            }
            var product = _productMapper.MapToModel(vm);
            product.ImageUrl = await _uploadFile.UploadImage(file);
            product.Description.Trim();
            await _productService.CreateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
