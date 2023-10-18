using Microsoft.AspNetCore.Mvc.Rendering;
using SupplyChain.App.Utils.Validations;
using SupplyChain.Core.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The Name field cannot exceed 50 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [MaxLength(1500, ErrorMessage = "The Name field cannot exceed 50 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Price field must be greater than or equal to 1.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Quantity field must be greater than or equal to 1.")]
        public decimal Quantity { get; set; }
        public decimal InputQuantity { get; set; }

        [Required(ErrorMessage = "Please Select Unit!")]
        [DisplayName("Unit")]
        public int UnitCode { get; set; }

        public string UnitName { get; set; }

        public SelectList Units { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ProductionDate { get; set; }

        [FutureDate(ErrorMessage = "The Expiration Date field must be a future date.")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "The Country of Origin field is required.")]
        [DisplayName("Country")]
        public string CountryOfOriginCode { get; set; }

        public string CountryOfOriginName { get; set; }

        public SelectList CountryOfOriginList { get; set; }

        [Required(ErrorMessage = "The Manufacturer field is required.")]
        [DisplayName("Manufacturer")]
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public SelectList ManufacturerList { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public SelectList CategoryList { get; set; }

        public ICollection<EventViewModel> events { get; set; }
        public int SupplierId { get; set; }
    }
}
