using SupplyChain.App.Utils.Validations;
using SupplyChain.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class ManufacturerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact name is required.")]
        [StringLength(50, ErrorMessage = "Contact name cannot be longer than 50 characters.")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Contact email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Contact phone is required.")]
        [CustomPhone(ErrorMessage = "Invalid phone number.")]
        public string ContactPhone { get; set; }
    }
}
