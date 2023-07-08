using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
