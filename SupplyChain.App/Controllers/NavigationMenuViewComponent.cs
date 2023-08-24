using Microsoft.AspNetCore.Mvc;

namespace SupplyChain.App.Controllers
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Retrieve data for navigation menu
            var menuItems = await GetMenuItemsAsync();

            // Pass the data to the view
            return View(menuItems);
        }
        private Task<List<MenuItem>> GetMenuItemsAsync()
        {
            // Retrieve and return your menu items from a data source
            // This could be from a database, a web service, or any other source
            // In this example, we'll just return a hardcoded list of MenuItem objects
            var menuItems = new List<MenuItem>
        {
            new MenuItem { Title = "Home", Url = "/" },
            new MenuItem { Title = "About", Url = "/about" },
            new MenuItem { Title = "Contact", Url = "/contact" }
        };

            return Task.FromResult(menuItems);
        }
    }

    internal class MenuItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
