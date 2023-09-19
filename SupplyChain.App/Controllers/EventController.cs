using Abp.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class EventController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IEventService _eventService;
        public EventController(IMapper mapper,
            IProductService productService,
            IEventService eventService)
        {
            _mapper = mapper;
            _productService = productService;
            _eventService = eventService;

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

                if (id > 0) //edit
                {
                    var entity = _eventService.GetEventByIdAsync(id);
                    vm = _mapper.Map<EventViewModel>(entity);
                }
                var products = await _productService.GetAllProductsLightWeightAsync();
                foreach (var product in products)
                {
                    vm.Products.Add(new ProductSelectedListViewModel { Id = product.Id, Name = product.Name });
                }
                vm.Start = DateTime.Now;
                vm.End = DateTime.Now;
                return PartialView("~/Views/Event/PartialViews/_AddEditEventForm.cshtml", vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ErrorResponse.PreException(ex));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditEvent(EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var ev = _mapper.Map<Event>(vm);

                if (ev.Id == 0) // Adding a new category
                {
                    try
                    {
                        var newId = await _eventService.CreateEventAsync(ev);
                        return Json(new ApiResponse<bool>(true, true, "An event was successfully created!"));
                    }
                    catch (Exception ex)
                    {
                        //rollback the transaction that caused the exception
                        await _eventService.RollbackTransaction();
                        return Json(new ApiResponse<bool>(false, false, ex.InnerException.Message.Trim(), "ERR001"));
                    }
                }
                else // Editing an existing category
                {
                    try
                    {
                        await _eventService.UpdateEventAsync(ev);
                        return Json(new ApiResponse<bool>(true, true, "An event was successfully updated!"));
                    }
                    catch (Exception ex)
                    {
                        await _eventService.RollbackTransaction();
                        return Json(new ApiResponse<bool>(false, false, $"Failed to update event \n {ex.InnerException.Message}"));
                    }

                }
            }
            else
            {
                return Json(new ApiResponse<bool>(false, false, "Please fill out all required fields."));
            }
        }

        [HttpGet]
        [DontWrapResult]
        public async Task<string> GetEvents(string start, string end)
        {
            DateTime _s = DateTime.Parse(start);
            DateTime _e = DateTime.Parse(end);
            var ev = await _eventService.GetIntervalEvent(_s, _e);
            string result = string.Empty;
            if (ev.Count() > 0)
            {
                List<CalenderViewModel> vm = new List<CalenderViewModel>();
                foreach (var item in ev)
                {
                    vm.Add(new CalenderViewModel
                    {
                        title = item.Title,
                        description = item.Description,
                        start = item.StartIn,
                        end = item.EndIn
                    });
                }
                result = JsonConvert.SerializeObject(vm);
            }
            return result;
        }
    }
}
