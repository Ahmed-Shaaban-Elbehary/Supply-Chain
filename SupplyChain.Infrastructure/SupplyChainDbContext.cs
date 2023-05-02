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
            // Define the Product entity
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(50);
            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(200);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .Property(p => p.ImageUrl)
                .HasMaxLength(200);
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductionDate)
                .HasColumnType("datetime2");
            modelBuilder.Entity<Product>()
                .Property(p => p.ExpirationDate)
                .HasColumnType("datetime2");
            modelBuilder.Entity<Product>()
                .Property(p => p.CountryOfOriginCode)
                .HasMaxLength(4);

            // Define the foreign key constraint for the manufacturer
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define the foreign key constraint for the product category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define the many-to-many relationship between products and components
            modelBuilder.Entity<ProductComponent>()
                .HasKey(pc => new { pc.ProductId, pc.ComponentId });
            modelBuilder.Entity<ProductComponent>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.Components)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductComponent>()
                .HasOne(pc => pc.Component)
                .WithMany()
                .HasForeignKey(pc => pc.ComponentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define the ProductCategory entity
            modelBuilder.Entity<ProductCategory>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<ProductCategory>()
                .Property(c => c.Name)
                .HasMaxLength(50);
            modelBuilder.Entity<ProductCategory>()
                .HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.Subcategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define the ShoppingCart entity
            modelBuilder.Entity<Cart>()
                .HasKey(sc => sc.Id);
            modelBuilder.Entity<Cart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Cart>()
                .Property(sc => sc.ShippingMethod)
                .HasMaxLength(50);
            modelBuilder.Entity<Cart>()
                .Property(sc => sc.ShippingAddress)
                .HasMaxLength(200);

            // Define the CartItem entity
            modelBuilder.Entity<CartItem>()
                .HasKey(sci => sci.Id);
            modelBuilder.Entity<CartItem>()
                .HasOne(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CartItem>()
                .HasOne(sci => sci.SourceWarehouse)
                .WithMany(w => w.OutgoingItems)
                .HasForeignKey(sci => sci.SourceWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CartItem>()
                .HasOne(sci => sci.DestinationWarehouse)
                .WithMany(w => w.IncomingItems)
                .HasForeignKey(sci => sci.DestinationWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define the Manufacturer entity
            modelBuilder.Entity<Manufacturer>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.Name)
                .HasMaxLength(50);
            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.Address)
                .HasMaxLength(200);
            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.ContactName)
                .HasMaxLength(50);
            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.ContactEmail)
                .HasMaxLength(50);
            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.ContactPhone)
                .HasMaxLength(50);

            // Define the Warehouse entity
            modelBuilder.Entity<Warehouse>()
                .HasKey(w => w.Id);
            modelBuilder.Entity<Warehouse>()
                .Property(w => w.Name)
                .HasMaxLength(50);
            modelBuilder.Entity<Warehouse>()
                .Property(w => w.Address)
                .HasMaxLength(200);

            // Define the User entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.Address)
                .HasMaxLength(200);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.Phone)
                .HasMaxLength(20);
        }
    }
}
