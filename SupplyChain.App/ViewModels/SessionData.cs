namespace SupplyChain.App.ViewModels
{
    public class SessionData
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> Permissions { get; set; } = Enumerable.Empty<string>();
    }
}
