using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SupplyChain.App.Utils.Validations
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = filterContext.HttpContext;
            // check  sessions here
            if (context.Session.GetString("userObj") == null)
            {
                filterContext.Result = new RedirectResult("~/Auth/TimeOut");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
