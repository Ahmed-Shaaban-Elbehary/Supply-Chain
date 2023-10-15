using Microsoft.AspNetCore.Mvc.Filters;

namespace SupplyChain.App.Utils.Validations
{
    /// <summary>
    /// Specifies that the class or method that this attribute, to avoiding user back again using browser back button, after logout.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NoCacheAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Clear chach control, expire-time, pragma.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            filterContext.HttpContext.Response.Headers["Expires"] = "-1";
            filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";

            base.OnResultExecuting(filterContext);
        }
    }
}
