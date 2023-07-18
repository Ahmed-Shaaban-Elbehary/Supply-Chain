using Microsoft.EntityFrameworkCore;
using SupplyChain.Core.Models;
using System.Data;
using System.Security;

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
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductComponent> ProductComponents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Define the Product entity
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(50);
            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(500);
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
            modelBuilder.Entity<Product>()
                .Property(p => p.Quantity)
                .HasColumnType("decimal(18,2)");
            #endregion

            #region Define the ProductCategory entity
            modelBuilder.Entity<ProductCategory>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<ProductCategory>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<ProductCategory>(e =>
            {
                e.HasIndex(pc => pc.Name).IsUnique();
            });
            #endregion

            #region Define the Manufacturer entity
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
            #endregion

            #region Define the Warehouse entity
            modelBuilder.Entity<Warehouse>()
                .HasKey(w => w.Id);
            modelBuilder.Entity<Warehouse>()
                .Property(w => w.Name)
                .HasMaxLength(50);
            modelBuilder.Entity<Warehouse>()
                .Property(w => w.Address)
                .HasMaxLength(200);
            #endregion

            #region Define the User entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Address)
                .HasMaxLength(200)
                .IsRequired(false);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Phone)
                .IsRequired(false)
                .HasMaxLength(20);
            modelBuilder.Entity<User>()
           .Property(u => u.IsSupplier)
           .IsRequired();
            #endregion

            #region Define the Permission entity
            modelBuilder.Entity<Permission>()
           .HasKey(p => p.Id);
            modelBuilder.Entity<Permission>()
                .HasIndex(p => p.Name)
                .IsUnique();
            modelBuilder.Entity<Permission>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            #endregion

            #region Define the Roles entity
            modelBuilder.Entity<Role>()
            .HasKey(r => r.Id);
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            #endregion

            #region Define the RolePermission entity
            modelBuilder.Entity<RolePermission>()
           .HasKey(rp => new { rp.RoleId, rp.PermissionId });
            #endregion

            #region Define the UserRole entity
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            #endregion

            #region Define the Notification entity
            modelBuilder.Entity<Notification>()
            .ToTable("Notifications")
            .HasKey(n => n.Id);

            modelBuilder.Entity<Notification>()
                .Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<Notification>()
                .Property(n => n.RecipientUserId)
                .IsRequired()
                .HasMaxLength(128);

            modelBuilder.Entity<Notification>()
                .Property(n => n.SenderUserId)
                .IsRequired()
                .HasMaxLength(128);

            modelBuilder.Entity<Notification>()
                .Property(n => n.Type)
                .IsRequired();

            modelBuilder.Entity<Notification>()
                .Property(n => n.CreatedDate)
                .IsRequired();

            #endregion Define the Notification entity
        }
    }
}
