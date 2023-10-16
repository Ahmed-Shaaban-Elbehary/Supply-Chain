namespace SupplyChain.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsSupplier { get; set; } //IsSupplier = false || true.
        public bool Deleted { get; set; } //IsSupplier = false || true.
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
