using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SupplyChain.App.App_Class;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Utils.Validations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InRoleAttribute : Attribute
    {
        private readonly string _roleName;
        private readonly ICurrentUserSerivce _session;

        public InRoleAttribute(string roleName)
        {
            _roleName = roleName;
        }
        public async Task OnActionExecutionAsync(AuthorizationFilterContext context)
        {
            bool isAuthorized = await Task.Run(() => _session.IsInRole(_roleName));
            if (!isAuthorized)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
