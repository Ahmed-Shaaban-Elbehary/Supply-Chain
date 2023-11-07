using SupplyChain.App.Utils.Validations;
using SupplyChain.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.ViewModels
{
    public class ProductQuantityRequestViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Quantity to Add is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity to Add must be a positive number")]
        public int QuantityToAdd { get; set; }

        [Required(ErrorMessage = "Request Date is required")]
        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime RequestIn { get; set; }
        public string RequestedBy { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get; set; }
        public string Status { get; set; } // Represent RequestStatus as a string

        // You can include additional properties from AssociatedEvent and Product if needed
        public int EventId { get; set; }
        public string EventName { get; set; } // Assuming you want to display the event name

        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
