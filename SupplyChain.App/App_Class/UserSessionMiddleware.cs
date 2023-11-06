using SupplyChain.Services.Contracts;

namespace SupplyChain.App.App_Class
{
    public class UserSessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserSessionService _userSessionService;

        public UserSessionMiddleware(RequestDelegate next, IUserSessionService userSessionService)
        {
            _next = next;
            _userSessionService = userSessionService;
        }

        public async Task Invoke(HttpContext context)
        {
            // Set the IUserSessionService in HttpContext for access in the attribute
            context.Items["UserSessionService"] = _userSessionService;
            await _next(context);
        }
    }
}
