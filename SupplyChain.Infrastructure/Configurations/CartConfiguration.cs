using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;
using System.Reflection.Emit;

namespace SupplyChain.Infrastructure.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(sc => sc.ShippingMethod).HasMaxLength(50);
            builder.Property(sc => sc.ShippingAddress).HasMaxLength(200);
            builder.Property(c => c.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasOne(sc => sc.User).WithMany(u => u.Carts).HasForeignKey(sc => sc.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Items).WithOne(ci => ci.Cart).HasForeignKey(ci => ci.CartId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
