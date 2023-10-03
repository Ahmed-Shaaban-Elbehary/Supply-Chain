using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SupplyChain.App.Notification;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class EventController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IEventService _eventService;
        private readonly IProductEventService _productEventService;
        private readonly IEventStatusService _eventStatusService;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly ILookUp _lookup;
        public EventController(IMapper mapper,
            IProductService productService,
            IEventService eventService,
            IProductEventService productEventService,
            IEventStatusService eventStatusService,
            IHubContext<NotificationHub> notificationHubContext,
            ILookUp lookup)
        {
            _mapper = mapper;
            _productService = productService;
            _eventService = eventService;
            _productEventService = productEventService;
            _eventStatusService = eventStatusService;
            _notificationHubContext = notificationHubContext;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddEditEvent(int id)
        {
            try
            {
                var vm = new EventViewModel();

                if (id > 0) //edit
                {
                    var entity = await _eventService.GetEventByIdAsync(id);
                    vm = _mapper.Map<EventViewModel>(entity);
                    var productEvents = await _productEventService.GetProductEventByEventIdAsync(id);
                    vm.ProductIds = productEvents.Select(pe => pe.ProductId).ToList();
                    vm.IsInEditMode = true;
                }
                else
                {
                    vm.IsInEditMode = false;
                    vm.StartIn = DateTime.Now;
                    vm.EndIn = DateTime.Now;
                }
                var products = await _productService.GetAllProductsLightWeightAsync();
                foreach (var product in products)
                {
                    vm.Products.Add(new ProductSelectedListViewModel { Id = product.Id, Name = product.Name });
                }
                return PartialView("~/Views/Event/PartialViews/_AddEditEventForm.cshtml", vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ErrorResponse.PreException(ex));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditEvent(EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var ev = _mapper.Map<Event>(vm);
                if (ev.Id == 0) // Adding a new event
                {
                    try
                    {
                        ev.CreatedBy = CurrentUser.GetUserId();
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
                        ev.PublishedIn = ev.Active ? DateTime.Now : null;
                        await _eventService.UpdateEventAsync(ev);
                        await _productEventService.UpdateProductEventAsync(ev, vm.ProductIds);

                        if (vm.Active)
                        {
                            vm.PublishedIn = DateTime.Now;
                            await _notificationHubContext.Clients.All
                                .SendAsync("sendToUser", vm);
                        }
                        else
                        {
                            int userId = CurrentUser.GetUserId();
                            var itemStatus = await _eventStatusService.GetEventStatusByEventIdAndUserIdAsync(vm.Id, userId);
                            if (itemStatus != null)
                            {
                                await _eventStatusService.DeleteEventStatusAsync(itemStatus.FirstOrDefault());
                            }
                        }

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
        public async Task<IActionResult> GetEventsList()
        {
            try
            {
                var _events = await _eventService.GetAllPagedEventsAsync();
                var vm = _mapper.Map<List<EventViewModel>>(_events);
                var currentUserId = CurrentUser.GetUserId();
                int esCounter = 0;
                foreach (var item in vm)
                {
                    var itemStatus = await _eventStatusService.GetEventStatusByEventIdAndUserIdAsync(item.Id, currentUserId);
                    if (itemStatus != null)
                    {
                        item.BackgroundColor = itemStatus.FirstOrDefault().MakeAsRead ? "bg-milky" : "bg-cloudy";
                        item.IsRemoved = itemStatus.FirstOrDefault().Removed;
                        esCounter++;
                    }
                    else
                    {
                        item.BackgroundColor = "bg-cloudy";
                    }
                }
               

                var model = new NotificationViewModel()
                {
                    EventCount = vm.Count,
                    DisplayGreenLight = vm.Count > esCounter,
                    EventViewModels = vm
                };

                return new JsonResult(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ErrorResponse.PreException(ex));
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
        public async Task<IActionResult> UpdateEventAsRead(int id)
        {
            try
            {
                int eventId = id;
                int currentUserId = CurrentUser.GetUserId();

                EventStatusViewModel ESvm = new EventStatusViewModel()
                {
                    UserId = currentUserId,
                    EventId = eventId,
                    MakeAsRead = true,
                    CreatedDate = DateTime.Now,
                };
                var eventStatus = _mapper.Map<EventStatus>(ESvm);
                //Add New Event Status
                await _eventStatusService.CreateEventStatusAsync(eventStatus);
                //Get Current Event
                var _event = await _eventService.GetEventByIdAsync(id);
                var Evm = _mapper.Map<EventViewModel>(_event);
                //Get Product For Event
                var productEvent = await _productEventService.GetProductEventByEventIdAsync(eventId);
                //Get Product List
                foreach (var item in productEvent)
                {
                    var product = await _productService.GetProductByIdAsync(item.ProductId);
                    var vm = _mapper.Map<ProductViewModel>(product);
                    vm.CountryOfOriginName = new SelectList(_lookup.Countries, "Code", "Name", product.CountryOfOriginCode)
                        .FirstOrDefault().Text;
                    vm.ManufacturerName = new SelectList(_lookup.Manufacturers, "Id", "Name", product.ManufacturerId)
                        .FirstOrDefault().Text;
                    vm.CategoryName = new SelectList(_lookup.Categories, "Id", "Name", product.CategoryId)
                        .FirstOrDefault().Text;

                    Evm.ProductViewModels.Add(vm);
                };

                return PartialView("~/Views/Event/PartialViews/_OpenEventDetails.cshtml", Evm);
            }
            catch (Exception ex)
            {
                await _eventStatusService.RollbackTransaction();
                return RedirectToAction("Index", "Error", ErrorResponse.PreException(ex));
            }
        }
    }
}
