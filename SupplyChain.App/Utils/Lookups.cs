using AutoMapper;
using Newtonsoft.Json;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Utils
{
    public class Lookups : ILookUp
    {
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IMapper _mapper;

        public Lookups(IWebHostEnvironment webHostEnvironment,
            IProductCategoryService productCategoryService,
            IManufacturerService manufacturerService,
            IMapper mapper
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _productCategoryService = productCategoryService;
            _manufacturerService = manufacturerService;
            _mapper = mapper;
            Manufacturers = GetManufacturers().Result.ToList();
            Categories = GetCategories().Result.ToList();
            Countries = GetCountries();
            Units = GetUnits();
        }

        public List<SelectItems> Countries { get; private set; }
        public List<SelectItems> Units { get; private set; }

        public List<ManufacturerViewModel> Manufacturers { get; private set; }

        public List<ProductCategoryViewModel> Categories { get; private set; }

        public List<SelectItems> GetCountries()
        {
            string jsonString = File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Countries.json"));
            List<SelectItems> countries = JsonConvert.DeserializeObject<List<SelectItems>>(jsonString);
            if (countries == null)
            {
                return new List<SelectItems>();
            }
            return countries;
        }
        public List<SelectItems> GetUnits()
        {
            string jsonString = File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Units.json"));
            List<SelectItems> units = JsonConvert.DeserializeObject<List<SelectItems>>(jsonString);
            if (units == null)
            {
                return new List<SelectItems>();
            }
            return units;
        }

        public async Task<IEnumerable<ManufacturerViewModel>> GetManufacturers()
        {
            var result = await _manufacturerService.GetAllManufacturerAsync();
            return _mapper.Map<IEnumerable<ManufacturerViewModel>>(result);
        }

        public async Task<IEnumerable<ProductCategoryViewModel>> GetCategories()
        {
            var result = await _productCategoryService.GetAllProductCategoriesAsync();
            return _mapper.Map<IEnumerable<ProductCategoryViewModel>>(result);
        }
    }
    public class SelectItems
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
