using Microsoft.AspNetCore.Mvc;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            var errorMessage = TempData["ErrorMessage"] as string;
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        public IActionResult Supplier(string t)
        {
            if (t != "Supplier")
            {
                TempData["ErrorMessage"] = "You Are Not Supplier!";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Supplier(User user)
        {
            return View();
        }

        public IActionResult Buyer(string t)
        {
            if (t != "Buyer")
            {
                TempData["ErrorMessage"] = "You Are Not Buyer!";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Buyer(User user)
        {
            return View();
        }
    }
}
