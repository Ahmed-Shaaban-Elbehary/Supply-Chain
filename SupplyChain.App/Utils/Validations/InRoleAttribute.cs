using Microsoft.AspNetCore.Mvc.Filters;
using SupplyChain.App.App_Class;
using SupplyChain.Services;

namespace SupplyChain.App.Utils.Validations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InRoleAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _roleName;

        public InRoleAttribute(string roleName)
        {
            _roleName = roleName;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool isAuthorized = await CurrentUserService.IsInRoleAsync(_roleName);

            if (!isAuthorized)
            {
                context.Result = new HtmlActionResult("Unauthorized");
                return;
            }

            await next();
        }
    }
}
