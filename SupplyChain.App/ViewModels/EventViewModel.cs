using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartIn { get; set; }
        [Required]
        public DateTime EndIn { get; set; }

        public bool Active { get; set; }
        public DateTime? PublishedIn { get; set; }

        public bool Deleted { get; set; }
        public List<UserViewModel> Users { get; set; }

        public string BackgroundColor { get; set; }

        [Required]
        public List<int> ProductIds { get; set; }
        public List<ProductViewModel> ProductViewModels { get; set; } = new List<ProductViewModel>();
        public List<ProductSelectedListViewModel> Products { get; set; } = new List<ProductSelectedListViewModel>();
    }

    public class ProductSelectedListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
