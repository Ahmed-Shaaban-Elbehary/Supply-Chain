using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.UserId).IsRequired();
            builder.Property(n => n.EventId).IsRequired();
            builder.Property(n => n.CreatedDate).IsRequired();


            builder.HasOne(n => n.Event).WithMany()
                .HasForeignKey(n => n.EventId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(n => n.User).WithMany()
                .HasForeignKey(n => n.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
