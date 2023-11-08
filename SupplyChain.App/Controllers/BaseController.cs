using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupplyChain.App.Utils.Validations;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        public BaseController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        [HttpGet]
        public void CustomException(Exception ex)
        {
            var errorResponse = ErrorResponse.PreException(ex);
            var errorResponseJson = JsonConvert.SerializeObject(errorResponse);
            TempData["ErrorResponse"] = errorResponseJson;
        }


        public int GetLoggedInUserId()
        {
            return _userSessionService.GetUserId();
        }

        public string GetLoggedInUserName()
        {
            return _userSessionService.GetUserName();
        }
    }
}
