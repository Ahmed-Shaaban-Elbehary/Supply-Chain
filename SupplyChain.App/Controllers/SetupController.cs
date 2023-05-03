using Microsoft.AspNetCore.Mvc;

namespace SupplyChain.App.Controllers
{
    public class SetupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manufacturer()
        {
            return View();
        }

        public IActionResult Category()
        {
            return View();
        }
    }
}
