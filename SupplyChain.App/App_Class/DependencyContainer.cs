using AutoMapper;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.App_Class
{
    public class DependencyContainer
    {
        public IProductCategoryService _productCategoryService { get; }
        public IManufacturerService _manufacturerService { get; }
        public IMapper _mapper { get; }

        public DependencyContainer(
            IProductCategoryService productCategoryService,
            IManufacturerService manufacturerService,
            IMapper mapper)
        {
            _productCategoryService = productCategoryService;
            _manufacturerService = manufacturerService;
            _mapper = mapper;
        }
    }
}
