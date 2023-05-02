﻿namespace SupplyChain.Core.Models
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
        public string CountryOfOriginCode { get; set; } = string.Empty;
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; } = new Manufacturer();
        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; } = new ProductCategory();
        public virtual List<ProductComponent> Components { get; set; } = new List<ProductComponent>();
    }
}
