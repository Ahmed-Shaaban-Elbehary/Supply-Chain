using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).HasMaxLength(50);
            builder.Property(u => u.Password).HasMaxLength(100);
            builder.Property(u => u.Address).HasMaxLength(200).IsRequired(false);
            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Phone).HasMaxLength(20).IsRequired(false);
            builder.Property(u => u.IsSupplier).IsRequired();

            builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Carts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserEvents)
                .WithOne(ue => ue.User)
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
