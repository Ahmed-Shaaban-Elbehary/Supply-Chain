using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Configurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Name).HasMaxLength(50);
            builder.Property(w => w.Address).HasMaxLength(200);
        }
    }
}
