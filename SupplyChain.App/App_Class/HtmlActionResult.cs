using Microsoft.AspNetCore.Mvc;

namespace SupplyChain.App.App_Class
{
    public class HtmlActionResult : IActionResult
    {
        private readonly string _viewName;

        public HtmlActionResult(string viewName)
        {
            _viewName = viewName;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var viewResult = new ViewResult
            {
                ViewName = _viewName
            };

            await viewResult.ExecuteResultAsync(context);
        }
    }
}
