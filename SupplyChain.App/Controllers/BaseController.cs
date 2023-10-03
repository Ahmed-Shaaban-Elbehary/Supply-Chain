using Microsoft.AspNetCore.Mvc;
using SupplyChain.Core.Models;

namespace SupplyChain.App.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            if (HttpContext is not null)
            {
                var userObj = HttpContext.Session.Keys.Count() > 0 ? HttpContext.Session.GetString("userObj") : null;
                string currentUri = Request.Path.ToString();

                if (userObj is null && currentUri != "/Auth/Login")
                {

                }
            }
            
        }
    }
}
