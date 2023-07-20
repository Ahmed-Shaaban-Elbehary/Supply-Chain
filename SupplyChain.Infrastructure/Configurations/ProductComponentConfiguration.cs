using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Configurations
{
    public class ProductComponentConfiguration : IEntityTypeConfiguration<ProductComponent>
    {
        public void Configure(EntityTypeBuilder<ProductComponent> builder)
        {
            builder.HasKey(pc => pc.Id);
            builder.Property(pc => pc.Quantity).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasOne(pc => pc.Component).WithMany().HasForeignKey(pc => pc.ComponentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pc => pc.Product).WithMany(p => p.Components).HasForeignKey(pc => pc.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
