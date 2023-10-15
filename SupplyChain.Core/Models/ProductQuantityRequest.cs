using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Core.Models
{
    public class ProductQuantityRequest
    {
        public int Id { get; set; } // Unique identifier for the request
        public int QuantityToAdd { get; set; } // The quantity to add
        public DateTime RequestIn { get; set; } // The date of the request
        public string RequestedBy { get; set; } // The person or entity making the request
        public string Reason { get; set; } // A description or reason for the request
        public RequestStatus Status { get; set; } // The status of the request (e.g., Pending, Approved, Denied)

        // Reference to the event associated with the request
        public int EventId { get; set; } // ID of the event
        public Event AssociatedEvent { get; set; } // Navigation property to the associated event

        public int ProductId { get; set; } // ID of the associated product
        public Product Product { get; set; } // Navigation property to the associated product
    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Denied
    }
}
