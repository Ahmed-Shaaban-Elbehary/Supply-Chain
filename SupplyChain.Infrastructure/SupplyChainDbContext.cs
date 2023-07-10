﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
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

            #region Define the foreign key constraint for the manufacturer
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Define the foreign key constraint for the product category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Define the many-to-many relationship between products and components
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
            modelBuilder.Entity<ProductComponent>()
                .Property(pc => pc.Quantity)
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

            #region Define the Cart entity
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
            #endregion

            #region Define the CartItem entity
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
            modelBuilder.Entity<CartItem>()
                .Property(c => c.Quantity)
                .HasColumnType("decimal(18,2)");
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
                .HasMaxLength(200);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Phone)
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
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
            #endregion

            #region Define the UserRole entity
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
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

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.RecipientUser)
                .WithMany()
                .HasForeignKey(n => n.RecipientUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.SenderUser)
                .WithMany()
                .HasForeignKey(n => n.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion Define the Notification entity
        }
    }
}
