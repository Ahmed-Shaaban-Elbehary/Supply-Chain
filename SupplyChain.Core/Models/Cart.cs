namespace SupplyChain.Core.Models
{
    public class Cart
    {
        private decimal _totalPrice;

        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartItem> Items { get; set; }
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        public string ShippingMethod { get; set; }
        public string ShippingAddress { get; set; }

        public void UpdateTotalPrice()
        {
            TotalPrice = Items.Sum(i => i.Product.Price * i.Quantity);
        }

    }
}
