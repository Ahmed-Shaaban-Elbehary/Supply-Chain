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
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedIn { get; set; }
        public bool Active { get; set; }
        public bool Published { get; set; }
        public bool Suspended { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
        public virtual ICollection<ProductEvent> ProductEvents { get; set; }
    }
}
