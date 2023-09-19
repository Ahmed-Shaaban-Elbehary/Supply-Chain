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
        private readonly IProductEventService _productEventService;
        public EventController(IMapper mapper,
            IProductService productService,
            IEventService eventService,
            IProductEventService productEventService)
        {
            _mapper = mapper;
            _productService = productService;
            _eventService = eventService;
            _productEventService = productEventService;

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
                    ViewBag.isEdit = true;
                    var entity = await _eventService.GetEventByIdAsync(id);
                    vm = _mapper.Map<EventViewModel>(entity);
                    var productEvents = await _productEventService.GetProductEventByEventIdAsync(id);
                    vm.ProductIds = productEvents.Select(pe => pe.ProductId).ToList();
                }
                else
                {
                    ViewBag.isEdit = false;
                }
                var products = await _productService.GetAllProductsLightWeightAsync();
                foreach (var product in products)
                {
                    vm.Products.Add(new ProductSelectedListViewModel { Id = product.Id, Name = product.Name });
                }
                vm.StartIn = DateTime.Now;
                vm.EndIn = DateTime.Now;
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

                if (ev.Id == 0) // Adding a new event
                {
                    try
                    {
                        var newId = await _eventService.CreateEventAsync(ev);
                        var newProductEvent = await _productEventService.AddProductEventAsync(ev, vm.ProductIds);
                        return Json(new ApiResponse<bool>(true, true, "An event was successfully created!"));
                    }
                    catch (Exception ex)
                    {
                        //rollback the transaction that caused the exception
                        await _eventService.RollbackTransaction();
                        return Json(new ApiResponse<bool>(false, false, ex.InnerException.Message.Trim(), "ERR001"));
                    }
                }
                else // Editing an existing event
                {
                    try
                    {
                        await _eventService.UpdateEventAsync(ev);
                        await _productEventService.UpdateProductEventAsync(ev, vm.ProductIds);
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
        public async Task<string> GetEvents(string start, string end)
        {
            DateTime _s = DateTime.Parse(start);
            DateTime _e = DateTime.Parse(end);
            try
            {
                var ev = await _eventService.GetIntervalEvent(_s, _e);
                string result = string.Empty;
                if (ev.Count() > 0)
                {
                    List<CalenderViewModel> vm = new List<CalenderViewModel>();
                    foreach (var item in ev)
                    {
                        vm.Add(new CalenderViewModel
                        {
                            id = item.Id,
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
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetEventById(int id)
        {
            try
            {
                var _event = await _eventService.GetEventByIdAsync(id);
                var productEvents = await _productEventService.GetProductEventByEventIdAsync(id);
                var vm = _mapper.Map<EventViewModel>(_event);
                vm.ProductIds = productEvents.Select(pe => pe.ProductId).ToList();
                return new JsonResult(vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ErrorResponse.PreException(ex));
            }
        }
    }
}
