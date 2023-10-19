using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using SupplyChain.Services;
using System.Security.Policy;

namespace SupplyChain.App.Utils.Validations
{
    /// <summary>
    ///  Specifies that the class or method that this attribute, to check session expiration, each user try to access a action.
    ///  in case the session expired will automatically redirect to time put page.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public SessionExpireAttribute(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = filterContext.HttpContext;

            // Check if the request is an AJAX request
            bool isAjaxRequest = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            var actionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;

            // Check if the action being executed is the "TimeOut" action
            bool isTimeoutAction = actionDescriptor?.ActionName == "TimeOut" && actionDescriptor?.ControllerName == "Auth";
            // Check if the action being executed is the "Login" action
            bool isLoginAction = actionDescriptor?.ActionName == "Login" && actionDescriptor?.ControllerName == "Auth"; // Adjust this based on your route configuration


            // Check sessions here
            if (!isLoginAction && !isTimeoutAction && context.Session.GetString("userObj") == null)
            {
                CurrentUser.Logout();

                var urlHelper = _urlHelperFactory.GetUrlHelper(filterContext);

                if (isAjaxRequest)
                {
                    // For AJAX requests, return a JSON result indicating a session timeout.
                    filterContext.Result = new JsonResult(new { redirectUrl = urlHelper.Action("TimeOut", "Auth") });
                }
                else
                {
                    // For full requests, redirect to the "TimeOut" page.
                    filterContext.Result = new RedirectResult(urlHelper.Action("TimeOut", "Auth"));
                }

                return;
            }

            base.OnActionExecuting(filterContext);
        }

    }
}
