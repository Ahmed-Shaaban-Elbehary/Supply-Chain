namespace SupplyChain.App.ViewModels
{
    public class PagedViewModel<T> where T : class
    {
        public IEnumerable<T> Model { get; set; }
        public int CurrentPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TotalPages { get; set; }
    }
}
