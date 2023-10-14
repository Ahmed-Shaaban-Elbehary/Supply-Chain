namespace SupplyChain.App.ViewModels
{
    public class NotificationViewModel
    {
        public bool DisplayGreenLight { get; set; }

        public int EventCount { get; set; }

        public List<EventViewModel> EventViewModels { get; set; } = new List<EventViewModel>();
    }
}
