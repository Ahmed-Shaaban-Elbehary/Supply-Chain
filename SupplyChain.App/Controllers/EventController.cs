using Microsoft.AspNetCore.Mvc;
using SupplyChain.App.ViewModels;

namespace SupplyChain.App.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> AddEditEvent(int id)
        {
            try
            {
                var vm = new EventViewModel();
                if (id > 0) //edit
                {

                }
                else
                {

                }
                return PartialView("~/Views/Event/PartialViews/_AddEditEventForm.cshtml", vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ErrorResponse.PreException(ex));
            }
        }
    }
}
