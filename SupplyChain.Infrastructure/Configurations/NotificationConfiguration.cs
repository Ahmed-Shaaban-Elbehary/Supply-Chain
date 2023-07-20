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
            builder.Property(n => n.Message).HasMaxLength(256).IsRequired();
            builder.Property(n => n.RecipientUserId).HasMaxLength(128).IsRequired();
            builder.Property(n => n.SenderUserId).HasMaxLength(128).IsRequired();
            builder.Property(n => n.Type).IsRequired();
            builder.Property(n => n.CreatedDate).IsRequired();
            builder.HasOne(n => n.SenderUser).WithMany().HasForeignKey(n => n.SenderUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(n => n.RecipientUser).WithMany().HasForeignKey(n => n.RecipientUserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
