using Microsoft.AspNetCore.Mvc;
using SupplyChain.App.Utils.Validations;
using SupplyChain.App.ViewModels;

namespace SupplyChain.App.Controllers
{
    public class ErrorController : BaseController
    {
        [NoCache]
        [SessionExpire]
        public IActionResult Index(ErrorResponse model)
        {
            return View(model);
        }
    }
}
