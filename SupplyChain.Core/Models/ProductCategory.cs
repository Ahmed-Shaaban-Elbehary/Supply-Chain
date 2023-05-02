namespace SupplyChain.Core.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }
        public virtual ProductCategory ParentCategory { get; set; }
        public virtual List<ProductCategory> Subcategories { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
