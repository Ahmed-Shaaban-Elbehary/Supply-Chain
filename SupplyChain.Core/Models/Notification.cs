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
        public string Message { get; set; }
        public string RecipientUserId { get; set; }
        public string SenderUserId { get; set; }
        public NotificationType Type { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public virtual User RecipientUser { get; set; }
        public virtual User SenderUser { get; set; }
    }
    public enum NotificationType
    {
        Email,
        SMS,
        InApp
    }
}
