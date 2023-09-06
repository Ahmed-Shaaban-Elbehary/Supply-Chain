using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.App.ViewModels;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class EventController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public EventController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> AddEditEvent(int id)
        {
            try
            {
                var vm = new EventViewModel();
                var products = await _productService.GetAllProductsAsync();
                foreach (var product in products)
                {
                    vm.Products.Add(new ProductSelectedListViewModel { Id = product.Id,  Name = product.Name });
                }
                if (id > 0) //edit
                {

                }
                else
                {

                }
                return PartialView("~/Views/Event/PartialViews/_AddEditEventForm.cshtml", vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ErrorResponse.PreException(ex));
            }
        }
    }
}
