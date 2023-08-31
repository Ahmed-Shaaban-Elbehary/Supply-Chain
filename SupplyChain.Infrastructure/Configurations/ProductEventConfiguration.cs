using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Infrastructure.Configurations
{
    public class ProductEventConfiguration : IEntityTypeConfiguration<ProductEvent>
    {
        public void Configure(EntityTypeBuilder<ProductEvent> builder)
        {
            builder.HasKey(pe => new { pe.ProductId, pe.EventId });

            builder.HasOne(pe => pe.Product)
                .WithMany(p => p.ProductEvents)
                .HasForeignKey(pe => pe.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pe => pe.Event)
                .WithMany(e => e.ProductEvents)
                .HasForeignKey(pe => pe.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
