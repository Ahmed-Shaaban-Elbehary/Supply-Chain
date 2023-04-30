namespace SupplyChain.Core.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = new Product();
        public decimal Quantity { get; set; }
        public int SourceWarehouseId { get; set; }
        public virtual Warehouse SourceWarehouse { get; set; } = new Warehouse();
        public int DestinationWarehouseId { get; set; }
        public virtual Warehouse DestinationWarehouse { get; set; } = new Warehouse();
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
