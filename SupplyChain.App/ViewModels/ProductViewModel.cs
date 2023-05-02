using Microsoft.AspNetCore.Mvc.Rendering;
using SupplyChain.App.Utils.Validations;
using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The Price field must be greater than or equal to 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The Quantity field must be greater than or equal to 0.")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "The ImageUrl field is required.")]
        [Url(ErrorMessage = "The ImageUrl field must be a valid URL.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "The Production Date field is required.")]
        public DateTime ProductionDate { get; set; }

        [Required(ErrorMessage = "The Expiration Date field is required.")]
        [FutureDate(ErrorMessage = "The Expiration Date field must be a future date.")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "The Country of Origin field is required.")]
        public int CountryOfOriginId { get; set; }

        public SelectList CountryOfOriginList { get; set; }

        [Required(ErrorMessage = "The Manufacturer field is required.")]
        public int ManufacturerId { get; set; }

        public SelectList ManufacturerList { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        public int CategoryId { get; set; }

        public SelectList CategoryList { get; set; }
    }
}
