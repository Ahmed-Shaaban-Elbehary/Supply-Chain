using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Core.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedIn { get; set; } = DateTime.Now;
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
        public virtual ICollection<ProductEvent> ProductEvents { get; set; }
    }
}
