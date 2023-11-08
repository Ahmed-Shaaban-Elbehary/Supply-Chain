using Microsoft.AspNetCore.Mvc.Filters;
using SupplyChain.App.App_Class;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Utils.Validations
{
    /// <summary>
    /// Specifies that the class or method that this attribute, to validate from user if user authority this action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InRoleAttribute : Attribute
    {
        private readonly string _roleName;

        public InRoleAttribute(string roleName)
        {
            _roleName = roleName;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Retrieve the IUserSessionService from HttpContext
            var userSessionService = context.HttpContext.Items["UserSessionService"] as IUserSessionService;

            if (userSessionService == null)
            {
                context.Result = new HtmlActionResult("Unauthorized");
                return;
            }

            bool isAuthorized = userSessionService.IsInRole(_roleName);

            if (!isAuthorized)
            {
                context.Result = new HtmlActionResult("Unauthorized");
                return;
            }
        }

    }
}
