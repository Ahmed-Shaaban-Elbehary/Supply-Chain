using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Infrastructure.Configurations
{
    public class ProductQuantityRequestConfiguration : IEntityTypeConfiguration<ProductQuantityRequest>
    {
        public void Configure(EntityTypeBuilder<ProductQuantityRequest> builder)
        {
            // Primary key
            builder.HasKey(pqr => pqr.Id);

            // Properties
            builder.Property(pqr => pqr.QuantityToAdd).IsRequired();
            builder.Property(pqr => pqr.RequestIn).IsRequired();
            builder.Property(pqr => pqr.RequestedBy).IsRequired();
            builder.Property(pqr => pqr.Reason).IsRequired();
            builder.Property(pqr => pqr.Status).IsRequired();

            // Configure the relationship with Event
            builder.HasOne(pqr => pqr.AssociatedEvent)
                .WithMany()
                .HasForeignKey(pqr => pqr.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship with Product
            builder.HasOne(pqr => pqr.Product)
                .WithMany()
                .HasForeignKey(pqr => pqr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
