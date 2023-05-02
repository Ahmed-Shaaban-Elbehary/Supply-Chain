namespace SupplyChain.Core.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<CartItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(i => i.Product.Price * i.Quantity);
        public string ShippingMethod { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
    }
}
