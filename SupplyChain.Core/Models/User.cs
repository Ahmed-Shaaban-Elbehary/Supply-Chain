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
        public bool IsSupplier { get; set; } = false; //IsSupplier = false || true.
        public ICollection<UserRole> UserRoles { get; set; }
        public virtual List<Cart> Carts { get; set; }
    }
}
