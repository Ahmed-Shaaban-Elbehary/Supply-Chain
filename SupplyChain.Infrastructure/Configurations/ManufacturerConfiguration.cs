using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Configurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).HasMaxLength(50);
            builder.Property(m => m.Address).HasMaxLength(200);
            builder.Property(m => m.ContactName).HasMaxLength(50);
            builder.Property(m => m.ContactEmail).HasMaxLength(50);
            builder.Property(m => m.ContactPhone).HasMaxLength(50);
        }
    }
}
