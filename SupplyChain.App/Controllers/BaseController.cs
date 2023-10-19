using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupplyChain.App.Utils.Validations;
using SupplyChain.App.ViewModels;

namespace SupplyChain.App.Controllers
{
    [ServiceFilter(typeof(NoCacheAttribute))]
    [ServiceFilter(typeof(SessionExpireAttribute))]
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
