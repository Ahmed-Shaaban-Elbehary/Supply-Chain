using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupplyChain.App.Mapper;
using SupplyChain.App.Mapper.Contracts;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.ViewModels;
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
            return View(vm);
        }

    }
}
