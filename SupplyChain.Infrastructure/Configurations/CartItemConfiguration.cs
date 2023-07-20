using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;
using System.Reflection.Emit;

namespace SupplyChain.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(c => c.Quantity).HasColumnType("decimal(18,2)");
            builder.Property(c => c.EstimatedDeliveryDate).HasColumnType("datetime2");
            builder.HasOne(sci => sci.Product).WithMany().HasForeignKey(sci => sci.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(sci => sci.SourceWarehouse).WithMany(w => w.OutgoingItems).HasForeignKey(sci => sci.SourceWarehouseId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(sci => sci.DestinationWarehouse).WithMany(w => w.IncomingItems).HasForeignKey(sci => sci.DestinationWarehouseId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
