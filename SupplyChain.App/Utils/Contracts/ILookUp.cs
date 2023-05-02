using static SupplyChain.App.Utils.Lookups;

namespace SupplyChain.App.Utils.Contracts
{
    public interface ILookUp
    {
        public List<Country> Countries { get; }

        public List<SelectList> Manufacturers { get; }

        public List<SelectList> Categories { get; }

        public List<Country> GetCountries();

        public List<SelectList> GetManufacturers();

        //public List<SelectList> GetCategories();
    }
}
