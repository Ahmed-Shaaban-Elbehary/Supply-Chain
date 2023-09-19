namespace SupplyChain.Core.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public virtual List<CartItem> IncomingItems { get; set; }
        public virtual List<CartItem> OutgoingItems { get; set; }
        public bool Deleted { get; set; }
    }
}
