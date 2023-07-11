using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsSupplier { get; set; } = false;
    }
}
