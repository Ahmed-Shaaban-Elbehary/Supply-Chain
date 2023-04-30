namespace SupplyChain.Core.Models
{
    public class ProductComponent
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = new Product();
        public int ComponentId { get; set; }
        public virtual Product Component { get; set; } = new Product();
        public decimal Quantity { get; set; }
    }
}
