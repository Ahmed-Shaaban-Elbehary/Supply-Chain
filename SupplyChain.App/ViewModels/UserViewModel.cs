using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public string Phone { get; set; } = null;
        public string Address { get; set; } = null;
        [Required]
        public bool IsSupplier { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
