namespace SupplyChain.Core.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public int SourceWarehouseId { get; set; }
        public virtual Warehouse SourceWarehouse { get; set; }
        public int DestinationWarehouseId { get; set; }
        public virtual Warehouse DestinationWarehouse { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
