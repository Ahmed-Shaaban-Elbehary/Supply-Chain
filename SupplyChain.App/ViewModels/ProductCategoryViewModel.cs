using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The Name field cannot exceed 100 characters.")]
        public string Name { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
