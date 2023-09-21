namespace SupplyChain.App.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public bool MakeAsRead { get; set; }
        public bool Removed { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
