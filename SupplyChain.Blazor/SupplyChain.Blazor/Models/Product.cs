namespace SupplyChain.Blazor.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CountryOfOriginCode { get; set; }
        public int UnitCode { get; set; }
        public bool Deleted { get; set; }
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<ProductComponent> Components { get; set; }
        public virtual ICollection<ProductEvent> ProductEvents { get; set; }

        public int SupplierId { get; set; }
        public virtual User Supplier { get; set; }
    }
}
