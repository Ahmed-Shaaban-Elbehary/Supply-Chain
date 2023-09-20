using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Event Event { get; set; }
    }
}
