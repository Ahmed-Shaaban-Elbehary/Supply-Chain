namespace SupplyChain.App.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Active { get; set; }
        public bool Published { get; set; }
        public bool Suspended { get; set; }
        public bool Deleted { get; set; }
        public List<UserViewModel> Users { get; set; }
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
