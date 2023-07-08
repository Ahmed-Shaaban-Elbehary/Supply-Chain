using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
