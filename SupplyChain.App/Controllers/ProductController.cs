using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.Utils.Validations;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILookUp _lookup;
        private readonly IUploadFile _uploadFile;
        public ProductController(IProductService productService,
            IMapper mapper,
            ILookUp lookUp,
            IUploadFile uploadFile
            )
        {
            _productService = productService;
            _mapper = mapper;
            _lookup = lookUp;
            _uploadFile = uploadFile;
        }

        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var categories = await _productService.GetAllPagedProductsAsync(page, pageSize);
            var vm = _mapper.Map<List<ProductViewModel>>(categories);
            vm.ForEach(item =>
            {
                item.UnitName = new SelectList(_lookup.Units, "Code", "Name", item.UnitCode).FirstOrDefault().Text;
            });

            var pagedModel = new PagedViewModel<ProductViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _productService.CountProductsAsync()
            };

            return View(pagedModel);
        }

        [HttpGet]
        public async Task<ActionResult> ProductItem(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var vm = _mapper.Map<ProductViewModel>(product);
            return View(vm);
        }

        [HttpGet]
        [InRole("admin")]
        public IActionResult Add()
        {
            var vm = new ProductViewModel();
            vm.CountryOfOriginList = new SelectList(_lookup.Countries, "Code", "Name");
            vm.ManufacturerList = new SelectList(_lookup.Manufacturers, "Id", "Name");
            vm.CategoryList = new SelectList(_lookup.Categories, "Id", "Name");
            vm.Units = new SelectList(_lookup.Units, "Code", "Name");
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
            vm.ImageUrl = await _uploadFile.UploadImage(file);
            vm.Description.Trim();
            vm.ProductName.Trim();
            await _productService.CreateProductAsync(_mapper.Map<Product>(vm));
            return RedirectToAction(nameof(Index));
        }
    }
}
