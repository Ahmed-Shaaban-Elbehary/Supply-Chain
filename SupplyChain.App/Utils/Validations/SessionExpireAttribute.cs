using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SupplyChain.Services;

namespace SupplyChain.App.Utils.Validations
{
    /// <summary>
    ///  Specifies that the class or method that this attribute, to check session expiration, each user try to access a action.
    ///  in case the session expired will automatically redirect to time put page.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// check session object is null or not, then redirect to TimeOut, also clear static user data.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = filterContext.HttpContext;
            // check  sessions here
            if (context.Session.GetString("userObj") == null)
            {
                CurrentUser.Logout();
                filterContext.Result = new RedirectResult("~/Auth/TimeOut");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
