using SupplyChain.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Product Name!")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "You Should Enter Product Price")]
        [Range(0, 9999)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "You Should Enter Product Quantity")]
        [Range(0, 9999)]
        public decimal Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        public string CountryOfOrigin { get; set; } = string.Empty;
    }
}
