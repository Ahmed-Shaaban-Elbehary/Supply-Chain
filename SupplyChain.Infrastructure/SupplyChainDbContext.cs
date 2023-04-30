using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Models;

namespace SupplyChain.Infrastructure
{
    public class SupplyChainDbContext : DbContext
    {
        public SupplyChainDbContext(DbContextOptions<SupplyChainDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductComponent> ProductComponents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the ProductCategory entity
            modelBuilder.Entity<ProductCategory>()
                .HasMany(pc => pc.Subcategories)
                .WithOne(pc => pc.ParentCategory)
                .HasForeignKey(pc => pc.ParentCategoryId);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(pc => pc.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // Configure the CartItem entity
            modelBuilder.Entity<CartItem>()
                .HasOne(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId);

            modelBuilder.Entity<CartItem>()
                .HasOne(sci => sci.SourceWarehouse)
                .WithMany(w => w.OutgoingItems)
                .HasForeignKey(sci => sci.SourceWarehouseId);

            modelBuilder.Entity<CartItem>()
                .HasOne(sci => sci.DestinationWarehouse)
                .WithMany(w => w.IncomingItems)
                .HasForeignKey(sci => sci.DestinationWarehouseId);

            // Configure the Cart entity
            modelBuilder.Entity<Cart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(sc => sc.UserId);

            // Configure the ProductComponent entity
            modelBuilder.Entity<ProductComponent>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.Components)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductComponent>()
                .HasOne(pc => pc.Component)
                .WithMany()
                .HasForeignKey(pc => pc.ComponentId);

            // Configure the Manufacturer entity
            modelBuilder.Entity<Manufacturer>()
                .HasMany(m => m.Products)
                .WithOne(p => p.Manufacturer)
                .HasForeignKey(p => p.ManufacturerId);
        }
    }
}
