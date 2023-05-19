namespace SupplyChain.App.ViewModels
{
    public class PagedViewModel<T> where T : class
    {

        public IEnumerable<T> Model { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public int StartPage => Math.Max(1, CurrentPage - 5);
        public int EndPage => Math.Min(TotalPages, CurrentPage + 4);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
