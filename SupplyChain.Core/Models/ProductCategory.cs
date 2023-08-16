﻿namespace SupplyChain.Core.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; }
    }
}
