using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SupplyChain.Services.Contracts;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public SessionExpireAttribute(IUrlHelperFactory urlHelperFactory, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _urlHelperFactory = urlHelperFactory;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// check session object is null or not, then redirect to TimeOut, also clear static user data.
        /// </summary>
        /// <param name="filterContext"></param>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isAjaxRequest = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            bool isTimeoutAction = actionDescriptor?.ActionName == "TimeOut" && actionDescriptor?.ControllerName == "Auth";
            bool isLoginAction = actionDescriptor?.ActionName == "Login" && actionDescriptor?.ControllerName == "Auth";
            bool isLogoutAction = actionDescriptor?.ActionName == "Logout" && actionDescriptor?.ControllerName == "Auth";

            // Check if a user-specific session token or cookie exists
            if (!UserHasSessionToken() && !isLoginAction && !isTimeoutAction && !isLogoutAction)
            {
                if (isAjaxRequest)
                {
                    var result = RenderPartialViewToString("_TimeOutPartialView", context);
                    context.Result = new ContentResult
                    {
                        Content = result,
                        ContentType = "text/html",
                    };
                }
                else
                {
                    var urlHelper = _urlHelperFactory.GetUrlHelper(context);
                    context.Result = new RedirectResult(urlHelper.Action("TimeOut", "Auth"));
                }

                return;
            }

            await next();
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

        private bool UserHasSessionToken()
        {
            // Check if the user has a session token (you may need to modify this logic based on your specific implementation)
            var sessionToken = _httpContextAccessor.HttpContext.Request.Cookies["UserSessionToken"];
            return !string.IsNullOrEmpty(sessionToken);
        }
    }
}