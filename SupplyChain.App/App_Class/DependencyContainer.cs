using AutoMapper;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.App_Class
{
    public class DependencyContainer
    {
        public IProductCategoryService _productCategoryService { get; }
        public IProductService _productService { get; }
        public IManufacturerService _manufacturerService { get; }
        public IMapper _mapper { get; }
        public IUploadFile _uploadFile { get; }
        public ILookUp _lookup { get; }
        public DependencyContainer(
            IProductCategoryService productCategoryService,
            IManufacturerService manufacturerService,
            IMapper mapper,
            IUploadFile uploadFile,
            IProductService productService,
            ILookUp lookup)
        {
            _productCategoryService = productCategoryService;
            _manufacturerService = manufacturerService;
            _mapper = mapper;
            _uploadFile = uploadFile;
            _productService = productService;
            _lookup = lookup;
        }
    }
}
