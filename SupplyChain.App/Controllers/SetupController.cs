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

        [HttpGet]
        public async Task<ActionResult> Category(int page = 1, int pageSize = 10)
        {
            int totalCount = await _productCategoryService.CountProductCategoryAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int currentPage = page;
            int startPage = currentPage - 5 > 0 ? currentPage - 5 : 1;
            int endPage = startPage + 9 < totalPages ? startPage + 9 : totalPages;

            var categories = await _productCategoryService.GetAllPagedProductCategoriesAsync(currentPage, pageSize);

            var vm = _productCategoryMapper.MapProductCategoryToViewModel(categories);

            var pagedModel = new PagedViewModel<ProductCategoryViewModel>
            {
                Model = vm,
                CurrentPage = currentPage,
                StartPage = startPage,
                EndPage = endPage,

                TotalPages = totalPages
            };

            return View(pagedModel);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var vm = new ProductCategoryViewModel();
            return View(vm);
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


        #region Methods
        [HttpPost]
        public async Task<PagedViewModel<ProductCategoryViewModel>> GetPagedProductCategory(int page = 1, int pageSize = 10)
        {
            int totalCount = await _productCategoryService.CountProductCategoryAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int currentPage = page;
            int startPage = currentPage - 5 > 0 ? currentPage - 5 : 1;
            int endPage = startPage + 9 < totalPages ? startPage + 9 : totalPages;

            var categories = await _productCategoryService.GetAllPagedProductCategoriesAsync(currentPage, pageSize);

            var vm = _productCategoryMapper.MapProductCategoryToViewModel(categories);

            var pagedModel = new PagedViewModel<ProductCategoryViewModel>
            {
                Model = vm,
                CurrentPage = currentPage,
                StartPage = startPage,
                EndPage = endPage,

                TotalPages = totalPages
            };

            return pagedModel;
        }
        #endregion
    }
}
