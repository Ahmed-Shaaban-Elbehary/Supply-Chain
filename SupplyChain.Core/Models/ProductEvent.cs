using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Core.Models
{
    public class ProductEvent
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
