using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SupplyChain.App.Notification;
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
        private readonly IEventStatusService _eventStatusService;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        public EventController(IMapper mapper,
            IProductService productService,
            IEventService eventService,
            IProductEventService productEventService,
            IEventStatusService eventStatusService,
            IHubContext<NotificationHub> notificationHubContext)
        {
            _mapper = mapper;
            _productService = productService;
            _eventService = eventService;
            _productEventService = productEventService;
            _eventStatusService = eventStatusService;
            _notificationHubContext = notificationHubContext;
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
                    ViewBag.isEdit = true;
                    var entity = await _eventService.GetEventByIdAsync(id);
                    vm = _mapper.Map<EventViewModel>(entity);
                    var productEvents = await _productEventService.GetProductEventByEventIdAsync(id);
                    vm.ProductIds = productEvents.Select(pe => pe.ProductId).ToList();
                }
                else
                {
                    ViewBag.isEdit = false;
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
                var _events = await _eventService.GetAllPagedEventsAsync(1, 10, (e => e.OrderByDescending(e => e.PublishedIn)));
                var vm = _mapper.Map<List<EventViewModel>>(_events);
                return new JsonResult(vm);
            }
            catch(Exception ex)
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

                NotificationViewModel vm = new NotificationViewModel();
                vm.UserId = currentUserId;
                vm.EventId = eventId;
                vm.MakeAsRead = true;
                vm.CreatedDate = DateTime.Now;
                var eventStatus = _mapper.Map<EventStatus>(vm);

                await _eventStatusService.CreateNotificationAsync(eventStatus);

                var _event = await _eventService.GetEventByIdAsync(id);
                var evm = _mapper.Map<EventViewModel>(_event);

                //need to get event products to display products image.
                //code!

                return PartialView("~/Views/Event/PartialViews/_OpenEventDetails.cshtml", evm);
            }
            catch (Exception ex)
            {
                await _eventStatusService.RollbackTransaction();
                return RedirectToAction("Index", "Error", ErrorResponse.PreException(ex));
            }
        }
    }
}
