using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Controllers
{
    public class BaseController : Controller
    {
        public void CustomException(Exception ex)
        {
            var errorResponse = ErrorResponse.PreException(ex);
            var errorResponseJson = JsonConvert.SerializeObject(errorResponse);
            TempData["ErrorResponse"] = errorResponseJson;
        }
    }
}
