using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Infrastructure.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.StartIn).IsRequired();
            builder.Property(e => e.EndIn).IsRequired();
            builder.Property(e => e.CreatedBy).IsRequired();
            builder.Property(e => e.CreatedIn).IsRequired();
            builder.Property(e => e.Active).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.Published).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.Suspended).IsRequired().HasDefaultValue(false);
            builder.Property(e => e.Deleted).IsRequired().HasDefaultValue(false);

            builder.HasMany(e => e.UserEvents)
                .WithOne(ue => ue.Event)
                .HasForeignKey(ue => ue.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.ProductEvents)
                .WithOne(pe => pe.Event)
                .HasForeignKey(pe => pe.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
