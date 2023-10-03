using Microsoft.AspNetCore.Mvc;
using SupplyChain.App.ViewModels;

namespace SupplyChain.App.Controllers
{
    public class ErrorController : BaseController
    {
        public IActionResult Index(ErrorResponse model)
        {
            return View(model);
        }
    }
}
