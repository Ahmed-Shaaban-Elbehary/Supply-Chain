using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SupplyChain.App.Notification;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.Utils.Validations;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    [ServiceFilter(typeof(SessionExpireAttribute))]
    [ServiceFilter(typeof(NoCacheAttribute))]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IEventService _eventService;
        private readonly IProductQuantityRequestService _productQuantityRequestService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILookUp _lookup;
        private readonly IUploadFile _uploadFile;
        public ProductController(
            IProductService productService,
            IEventService eventService,
            IProductQuantityRequestService productQuantityRequestService,
            IUserService userService,
            IMapper mapper,
            ILookUp lookUp,
            IUploadFile uploadFile,
            IUserSessionService userSessionService) : base(userSessionService)
        {
            _productService = productService;
            _eventService = eventService;
            _productQuantityRequestService = productQuantityRequestService;
            _userService = userService;
            _mapper = mapper;
            _lookup = lookUp;
            _uploadFile = uploadFile;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var products = await _productService.GetAllPagedProductsAsync(page, pageSize);
            var vm = _mapper.Map<List<ProductViewModel>>(products);
            vm.ForEach(item =>
            {
                item.UnitName = new SelectList(_lookup.Units, "Code", "Name", item.UnitCode).FirstOrDefault().Text;
            });

            var pagedModel = new PagedViewModel<ProductViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _productService.CountProductsAsync()
            };

            return View(pagedModel);
        }

        [HttpGet]
        public async Task<ActionResult> ProductItem(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var _events = await _eventService.GetProductEventsAsync(product.Id);
            var vm = _mapper.Map<ProductViewModel>(product);
            vm.events = _mapper.Map<List<EventViewModel>>(_events);
            return View(vm);
        }

        [HttpGet]
        [InRole("admin")]
        public IActionResult Add()
        {
            var vm = new ProductViewModel();
            vm.CountryOfOriginList = new SelectList(_lookup.Countries, "Code", "Name");
            vm.ManufacturerList = new SelectList(_lookup.Manufacturers, "Id", "Name");
            vm.CategoryList = new SelectList(_lookup.Categories, "Id", "Name");
            vm.Units = new SelectList(_lookup.Units, "Code", "Name");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductViewModel vm, IFormFile file)
        {
            try
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (file == null || file.Length == 0 || !allowedExtensions.Contains(extension))
                {
                    return BadRequest("Invalid file type.");
                }
                vm.ImageUrl = await _uploadFile.UploadImage(file);
                vm.Description.Trim();
                vm.ProductName.Trim();
                vm.SupplierId = GetLoggedInUserId();
                await _productService.CreateProductAsync(_mapper.Map<Product>(vm));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await _productService.RollbackTransaction();
                CustomException(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        #region Additional Quantity Requests

        [HttpGet]
        public async Task<ActionResult> Requests(int page = 1, int pageSize = 10)
        {
            try
            {
                var productQuantityRequests = await _productQuantityRequestService.GetAllPagedProductQuantityRequestsAsync(page, pageSize);
                var vm = _mapper.Map<List<ProductQuantityRequestViewModel>>(productQuantityRequests);
                foreach (var item in vm)
                {
                    var user = await _userService.GetUserByIdAsync(item.RequestedBy);
                    item.RequestedByName = user != null ? user.Name : "";
                    item.UnitName = new SelectList(_lookup.Units, "Code", "Name", item.UnitCode).FirstOrDefault().Text;
                }
                var pagedModel = new PagedViewModel<ProductQuantityRequestViewModel>
                {
                    Model = vm,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = await _productQuantityRequestService.CountProductQuantityAsync()
                };

                return View(pagedModel);
            }
            catch (Exception ex)
            {
                await _productQuantityRequestService.RollbackTransaction();
                CustomException(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> RequestAdditionProductQuantities(int id)
        {
            try
            {
                var vm = new ProductQuantityRequestViewModel();
                vm.ProductId = id;
                return PartialView("~/Views/Product/PartialViews/_AddProductQuantity.cshtml", vm);
            }
            catch (Exception ex)
            {
                await _productService.RollbackTransaction();
                CustomException(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProductQuantityRequest(ProductQuantityRequestViewModel vm)
        {
            if (vm.QuantityToAdd >= 0)
            {
                try
                {
                    vm.Status = RequestStatus.Pending.ToString();
                    vm.RequestedBy = GetLoggedInUserId();
                    vm.RequestIn = DateTime.Now;

                    //Add New Request.
                    var productQuantityRequest = _mapper.Map<ProductQuantityRequest>(vm);
                    await _productQuantityRequestService.CreateProductQuantityRequestAsync(productQuantityRequest);

                    //Return Notification View Model.
                    var product = await _productService.GetProductByIdAsync(vm.ProductId);
                    var mvm = new MessageViewModel()
                    {
                        Sender = GetLoggedInUserName(),
                        Receiver = product.SupplierId,
                        MessageTitle = product.Name,
                        MessageBody = vm.QuantityToAdd.ToString()
                    };

                    return Json(new ApiResponse<MessageViewModel>(true, mvm, "Request Sent Success"));
                }
                catch (Exception ex)
                {
                    await _productQuantityRequestService.RollbackTransaction();
                    return Json(new ApiResponse<bool>(false, false, $"Failed to update user \n {ex.InnerException.Message}"));
                }
            }
            else
            {
                return Json(new ApiResponse<bool>(false, false, $"Please enter Additional Quantity!"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRequestProductCardData()
        {
            const int page = 1;
            const int pageSize = 10;
            var productQuantityRequests = await _productQuantityRequestService.GetAllPagedProductQuantityRequestsAsync(page, pageSize);
            var vm = _mapper.Map<List<ProductQuantityRequestViewModel>>(productQuantityRequests);
            foreach (var item in vm)
            {
                var user = await _userService.GetUserByIdAsync(item.RequestedBy);
                item.RequestedByName = user != null ? user.Name : "";
                item.UnitName = new SelectList(_lookup.Units, "Code", "Name", item.UnitCode).FirstOrDefault().Text;
            }
            var pagedModel = new PagedViewModel<ProductQuantityRequestViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _productQuantityRequestService.CountProductQuantityAsync()
            };
            return PartialView("~/Views/Product/PartialViews/_RequestsCardPatialView.cshtml", pagedModel);
        }

        [HttpGet]
        public async Task<JsonResult> AcceptRequest(int id)
        {
            if (id > 0)
            {
                try
                {
                    var request = await _productQuantityRequestService.GetProductQuantityRequestByIdAsync(id);
                    request.Status = RequestStatus.Approved;
                    await _productQuantityRequestService.UpdateProductQuantityRequestAsync(request);
                    return Json(new ApiResponse<bool>(true, true, "A request was successfully approved"));
                }
                catch (Exception ex)
                {
                    return Json(new ApiResponse<bool>(false, false, $"Failed to accept the request \n {ex.InnerException.Message}"));
                }

            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<JsonResult> IgnoreRequest(int id)
        {
            if (id > 0)
            {
                try
                {
                    var request = await _productQuantityRequestService.GetProductQuantityRequestByIdAsync(id);
                    request.Status = RequestStatus.Denied;
                    await _productQuantityRequestService.UpdateProductQuantityRequestAsync(request);
                    return Json(new ApiResponse<bool>(true, true, "A request was successfully denied"));
                }
                catch (Exception ex)
                {
                    return Json(new ApiResponse<bool>(false, false, $"Failed to reject the request \n {ex.InnerException.Message}"));
                }

            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion Additional Quantity Requests
    }
}
