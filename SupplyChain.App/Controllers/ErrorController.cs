using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupplyChain.App.Utils.Validations;
using SupplyChain.App.ViewModels;

namespace SupplyChain.App.Controllers
{
    public class ErrorController : BaseController
    {
        public IActionResult Index()
        {
            if (TempData.TryGetValue("ErrorResponse", out var errorResponseJson))
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorResponseJson.ToString());
                return View(errorResponse);
            }

            // If TempData doesn't contain an error response, handle it as needed.
            return RedirectToAction("/Auth/Login");
        }
    }
}
