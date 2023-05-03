using Newtonsoft.Json;
using SupplyChain.App.Mappers;
using SupplyChain.App.Mappers.Contracts;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Utils
{
    public class Lookups : ILookUp
    {
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductCategoryMapper _productCategoryMapper;

        public Lookups(IWebHostEnvironment webHostEnvironment, 
            IProductCategoryService productCategoryService,
            IProductCategoryMapper productCategoryMapper
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _productCategoryService = productCategoryService;
            _productCategoryMapper = productCategoryMapper;
            Countries = GetCountries();
            Manufacturers = GetManufacturers();
            Categories = GetCategories().Result.ToList();
        }

        public List<Country> Countries { get; private set; }

        public List<SelectList> Manufacturers { get; private set; }

        public List<ProductCategoryViewModel> Categories { get; private set; }

        public List<Country> GetCountries()
        {
            string jsonString = File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "Countries.json"));
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(jsonString);
            if (countries == null)
            {
                return new List<Country>();
            }
            return countries;
        }

        public List<SelectList> GetManufacturers()
        {
            return new List<SelectList>()
            {
                new SelectList { Id = 1, Name = "Misr Linen"},
                new SelectList { Id = 2, Name = "El Nasr Clothing and Textile Company (Kabo)"},
                new SelectList { Id = 3, Name = "El Nasr Wool and Selected Textiles Company"},
                new SelectList { Id = 4, Name = "El-Mahalla El-Kubra Spinning and Weaving Company"},
                new SelectList { Id = 5, Name = "Alexandria Spinning and Weaving Company"},
            };
        }

        public async Task<IEnumerable<ProductCategoryViewModel>> GetCategories()
        {
            var result = await _productCategoryService.GetAllProductCategoriesAsync();
            return _productCategoryMapper.MapProductCategoryToViewModel(result);
        }

        public class Country
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class SelectList
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
