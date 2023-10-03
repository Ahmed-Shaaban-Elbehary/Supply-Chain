using SupplyChain.Services;
using System.Security.Policy;

namespace SupplyChain.App.App_Class
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var userId = context.Session.Keys.Count() > 0 ?
                context.Session.GetString("LoggedUserID").ToString() :
                string.Empty;

            if ((!CurrentUser.IsLoggedIn() && string.IsNullOrEmpty(userId))
                && !context.Request.Path.StartsWithSegments("/Auth/Login"))
            {
                string url = $"{context.Request.Scheme}://{context.Request.Host}/Auth/Login";
                context.Response.Redirect(url);
                return;
            }

            await _next(context);
        }
    }
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
