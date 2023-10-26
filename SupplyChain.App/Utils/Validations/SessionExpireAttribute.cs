using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        private readonly IServiceProvider _serviceProvider;

        public SessionExpireAttribute(IUrlHelperFactory urlHelperFactory, IServiceProvider serviceProvider)
        {
            _urlHelperFactory = urlHelperFactory;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// check session object is null or not, then redirect to TimeOut, also clear static user data.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = filterContext.HttpContext;

            bool isAjaxRequest = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            var actionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;

            bool isTimeoutAction = actionDescriptor?.ActionName == "TimeOut" && actionDescriptor?.ControllerName == "Auth";
            bool isLoginAction = actionDescriptor?.ActionName == "Login" && actionDescriptor?.ControllerName == "Auth";
            bool isLogoutAction = actionDescriptor?.ActionName == "Logout" && actionDescriptor?.ControllerName == "Auth";
            bool isSessionNull = context.Session.GetString("userObj") == null;

            if (isTimeoutAction && isSessionNull)
            {
                var urlHelper = _urlHelperFactory.GetUrlHelper(filterContext);
                filterContext.Result = new RedirectResult(urlHelper.Action("Logout", "Auth"));
            }
            else if (!isLoginAction && !isTimeoutAction && !isLogoutAction && isSessionNull)
            {
                // CurrentUser.Logout();

                if (isAjaxRequest)
                {
                    // For AJAX requests, render a partial view and return its HTML content.
                    var result = RenderPartialViewToString("_TimeOutPartialView", filterContext);
                    filterContext.Result = new ContentResult
                    {
                        Content = result,
                        ContentType = "text/html",
                    };
                }
                else
                {
                    var urlHelper = _urlHelperFactory.GetUrlHelper(filterContext);
                    filterContext.Result = new RedirectResult(urlHelper.Action("TimeOut", "Auth"));
                }

                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private string RenderPartialViewToString(string viewName, ActionExecutingContext filterContext)
        {
            var controller = (Controller)filterContext.Controller;

            var viewEngine = _serviceProvider.GetRequiredService<ICompositeViewEngine>();
            var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

            if (viewResult.Success)
            {
                using (var sw = new StringWriter())
                {
                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw, new HtmlHelperOptions());

                    viewResult.View.RenderAsync(viewContext).GetAwaiter().GetResult();
                    return sw.GetStringBuilder().ToString();
                }
            }
            else
            {
                // Handle the case where the view cannot be found.
                return "View not found: " + viewName;
            }
        }
    }
}