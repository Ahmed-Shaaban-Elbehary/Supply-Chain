using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.ImageUrl).HasMaxLength(200);
            builder.Property(p => p.ProductionDate).HasColumnType("datetime2");
            builder.Property(p => p.ExpirationDate).HasColumnType("datetime2");
            builder.Property(p => p.CountryOfOriginCode).HasMaxLength(4);
            builder.Property(p => p.Quantity).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Deleted).IsRequired().HasDefaultValue(false);
            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Manufacturer).WithMany(m => m.Products).HasForeignKey(p => p.ManufacturerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Supplier).WithMany(u => u.Products).HasForeignKey(p => p.SupplyId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
