﻿namespace SupplyChain.Core.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public bool Deleted { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
