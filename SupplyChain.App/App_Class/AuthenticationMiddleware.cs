using SupplyChain.Services;

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
            if (!CurrentUser.IsLoggedIn() && !context.Request.Path.StartsWithSegments("/Auth/Login"))
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
