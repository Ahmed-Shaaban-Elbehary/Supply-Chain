namespace SupplyChain.App.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public List<ProductCategoryViewModel> Subcategories { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
