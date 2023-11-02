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
    public class InRoleAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _roleName;
        private readonly IUserSessionService _userSessionService;
        public InRoleAttribute(string roleName)
        {
            _roleName = roleName;
        }
        public InRoleAttribute(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }
        /// <summary>
        /// Redirect to Un-authorization page, in case the user not authorized.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns>Page</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool isAuthorized = await _userSessionService.IsInRoleAsync(_roleName);

            if (!isAuthorized)
            {
                context.Result = new HtmlActionResult("Unauthorized");
                return;
            }

            await next();
        }
    }
}
