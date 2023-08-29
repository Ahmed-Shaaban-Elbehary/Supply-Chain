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
            // Configure table name
            builder.ToTable("Events");

            // Configure primary key
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Start)
                .IsRequired();

            builder.Property(e => e.End)
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .IsRequired();

            builder.Property(e => e.CreatedIn)
                .IsRequired();

            builder.Property(e => e.Active)
                .IsRequired();

            builder.Property(e => e.Published)
                .IsRequired();

            builder.Property(e => e.Suspended)
                .IsRequired();

            builder.Property(e => e.Deleted)
                .IsRequired();

            // Configure relationships
            builder.HasMany(e => e.Products)
                .WithMany(p => p.Events)
                .UsingEntity(j => j.ToTable("ProductEvent"));

            builder.HasMany(e => e.Users)
                .WithMany(u => u.Events)
                .UsingEntity(j => j.ToTable("UserEvent"));

        }
    }
}
