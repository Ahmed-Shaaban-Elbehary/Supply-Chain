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

    }
}
