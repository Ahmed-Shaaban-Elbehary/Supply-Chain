using Microsoft.AspNetCore.Mvc;

namespace SupplyChain.App.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
