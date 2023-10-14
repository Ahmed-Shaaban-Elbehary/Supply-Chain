using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.App.Utils.Validations;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class SetupController : BaseController
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;
        public SetupController(IProductCategoryService productCategoryService,
            IMapper mapper,
            IManufacturerService manufacturerService,
            IUserService userService,
            IRoleService roleService,
            IUserRoleService userRoleService)
        {
            _productCategoryService = productCategoryService;
            _mapper = mapper;
            _manufacturerService = manufacturerService;
            _userService = userService;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        [NoCache]
        [SessionExpire]
        public IActionResult Index()
        {
            return View();
        }

        #region Category
        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<ActionResult> Category(int page = 1, int pageSize = 10)
        {
            var categories = await _productCategoryService.GetAllPagedProductCategoriesAsync(page, pageSize);
            var vm = _mapper.Map<List<ProductCategoryViewModel>>(categories);

            var pagedModel = new PagedViewModel<ProductCategoryViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _productCategoryService.CountProductCategoryAsync()
            };

            return View(pagedModel);
        }

        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<ActionResult> AddEditCategory(int id)
        {
            var vm = new ProductCategoryViewModel();

            if (id > 0)
            {
                var category = await _productCategoryService.GetProductCategoryByIdAsync(id);
                vm = _mapper.Map<ProductCategoryViewModel>(category);
            }

            return PartialView("~/Views/Setup/PartialViews/_AddEditCategoryForm.cshtml", vm);
        }

        [HttpPost]
        [SessionExpire]
        [NoCache]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddEditCategory(ProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<ProductCategory>(vm);

                if (category.Id == 0) // Adding a new category
                {
                    try
                    {
                        await _productCategoryService.CreateProductCategoryAsync(category);
                        return Json(new ApiResponse<bool>(true, true, "Add New Category Successed!"));
                    }
                    catch (Exception ex)
                    {
                        //rollback the transaction that caused the exception
                        await _productCategoryService.RollbackTransaction();
                        return Json(new ApiResponse<bool>(false, false, $"Failed to add category \n {ex.InnerException.Message.Trim()}", "ERR001"));
                    }
                }
                else // Editing an existing category
                {
                    try
                    {
                        await _productCategoryService.UpdateProductCategoryAsync(category);
                        return Json(new ApiResponse<bool>(true, true, "Edit Category Successed!"));
                    }
                    catch (Exception ex)
                    {
                        //rollback the transaction that caused the exception
                        await _productCategoryService.RollbackTransaction();
                        return Json(new ApiResponse<bool>(false, false, $"Failed to edit category \n {ex.InnerException.Message.Trim()}", "ERR001"));
                    }

                }
            }
            return Json(new { message = "Oops, Error Occurred, Please Try Again!" });
        }

        [HttpDelete]
        [NoCache]
        [SessionExpire]
        public async Task<JsonResult> DeleteCategory(int id)
        {
            if (id > 0)
            {
                try
                {
                    var productCategory = await _productCategoryService.GetProductCategoryByIdAsync(id);
                    await _productCategoryService.DeleteProductCategoryAsync(productCategory);
                    return Json(new ApiResponse<bool>(true, true, "A category was Successfully Deleted"));
                }
                catch (Exception ex)
                {
                    await _userService.RollbackTransaction();
                    return Json(new ApiResponse<bool>(false, false, $"Failed to delete category \n {ex.InnerException.Message}"));
                }
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<IActionResult> GetCategoriesCardData()
        {
            const int page = 1;
            const int pageSize = 10;
            var categories = await _productCategoryService.GetAllPagedProductCategoriesAsync(page, pageSize);
            var vm = _mapper.Map<List<ProductCategoryViewModel>>(categories);
            var pagedModel = new PagedViewModel<ProductCategoryViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _productCategoryService.CountProductCategoryAsync()
            };
            return PartialView("~/Views/Setup/PartialViews/_CategoryCardPatialView.cshtml", pagedModel);
        }

        #endregion Category

        #region Manufacturer
        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<IActionResult> Manufacturer(int page = 1, int pageSize = 10)
        {
            var manufacturers = await _manufacturerService.GetAllPagedManufacturerAsync(page, pageSize);
            var vm = _mapper.Map<List<ManufacturerViewModel>>(manufacturers);
            var pagedModel = new PagedViewModel<ManufacturerViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _manufacturerService.CountManufacturerAsync()
            };

            return View(pagedModel);
        }

        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<ActionResult> AddEditManufacturer(int id)
        {
            var vm = new ManufacturerViewModel();

            if (id > 0)
            {
                var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
                vm = _mapper.Map<ManufacturerViewModel>(manufacturer);
            }

            return PartialView("~/Views/Setup/PartialViews/_AddEditManufacturerForm.cshtml", vm);
        }

        [HttpPost]
        [NoCache]
        [SessionExpire]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddEditManufacturer(ManufacturerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var manufacturer = _mapper.Map<Manufacturer>(vm);

                if (manufacturer.Id == 0) // Adding a new category
                {
                    try
                    {
                        await _manufacturerService.CreateManufacturerAsync(manufacturer);
                        return Json(new ApiResponse<bool>(true, true, "Add New Manufacturer Successed!"));

                    }
                    catch (Exception ex)
                    {
                        await _userService.RollbackTransaction();
                        return Json(new ApiResponse<bool>(false, false, $"Failed to add manufacturer \n {ex.InnerException.Message}"));
                    }
                }
                else // Editing an existing category
                {
                    try
                    {
                        await _manufacturerService.UpdateManufacturerAsync(manufacturer);
                        return Json(new { message = "Edit Manufacturer Successed!" });
                    }
                    catch (Exception ex)
                    {
                        await _userService.RollbackTransaction();
                        return Json(new ApiResponse<bool>(false, false, $"Failed to edit manufacturer \n {ex.InnerException.Message}"));
                    }

                }

            }

            // If the model is not valid, redisplay the form with validation errors
            //ViewBag.Manufacturers = await _manufacturerService.GetAllManufacturerAsync();
            return Json(new { message = "Oops, Error Occurred, Please Try Again!" });
        }

        [HttpDelete]
        [NoCache]
        [SessionExpire]
        public async Task<JsonResult> DeleteManufacturer(int id)
        {
            if (id > 0)
            {
                try
                {
                    var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
                    await _manufacturerService.DeleteManufacturerAsync(manufacturer);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    await _manufacturerService.RollbackTransaction();
                    return Json(new ApiResponse<bool>(false, false, $"Failed to delete manufacturer \n {ex.InnerException.Message}"));
                }

            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<IActionResult> GetManufacturerCardData()
        {
            const int page = 1;
            const int pageSize = 10;
            var manufacturers = await _manufacturerService.GetAllPagedManufacturerAsync(page, pageSize);
            var vm = _mapper.Map<List<ManufacturerViewModel>>(manufacturers);
            var pagedModel = new PagedViewModel<ManufacturerViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _manufacturerService.CountManufacturerAsync()
            };
            return PartialView("~/Views/Setup/PartialViews/_ManufacturerCardPatialView.cshtml", pagedModel);
        }

        #endregion Manufacturer

        #region Users

        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<IActionResult> Users(int page = 1, int pageSize = 10)
        {
            var users = await _userService.GetAllPagedUsersAsync(page, pageSize);
            var vm = _mapper.Map<List<UserViewModel>>(users);
            var pagedModel = new PagedViewModel<UserViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _userService.CountUserAsync()
            };
            return View(pagedModel);
        }

        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<ActionResult> AddEditUser(int id)
        {
            try
            {
                var vm = new UserViewModel();
                if (id > 0)
                {
                    ViewBag.isEdit = true;
                    var user = await _userService.GetUserByIdAsync(id);
                    vm = _mapper.Map<UserViewModel>(user);
                    vm.RoleIds = user.UserRoles.Select(e => e.RoleId).ToList();
                }
                else
                {
                    ViewBag.isEdit = false;
                }
                var roles = await _roleService.GetAllRolesAsync();
                foreach (var role in roles)
                {
                    vm.Roles.Add(new RolesViewModel { Id = role.Id, Name = role.Name });
                }
                return PartialView("~/Views/Setup/PartialViews/_AddEditUserForm.cshtml", vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ErrorResponse.PreException(ex));
            }
        }

        [HttpPost]
        [SessionExpire]
        [NoCache]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEditUser(UserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(vm);

                if (user.Id == 0) // Adding a new category
                {
                    try
                    {
                        var newId = await _userService.CreateUserAsync(user, user.Password);
                        var newRoles = await _userRoleService.AddUserRolesAsync(user, vm.RoleIds);
                        return Json(new ApiResponse<bool>(true, true, "A user was successfully created!"));
                    }
                    catch (Exception ex)
                    {
                        //rollback the transaction that caused the exception
                        await _userRoleService.RollbackTransaction();
                        return Json(new ApiResponse<bool>(false, false, ex.InnerException.Message.Trim(), "ERR001"));
                    }
                }
                else // Editing an existing category
                {
                    try
                    {
                        await _userService.UpdateUserAsync(user, vm.Password, vm.RoleIds, vm.IsPasswordChanged);
                        await _userRoleService.UpdateUserRolesAsync(user, vm.RoleIds);
                        return Json(new ApiResponse<bool>(true, true, "A user was successfully updated!"));
                    }
                    catch (Exception ex)
                    {
                        await _userService.RollbackTransaction();
                        return Json(new ApiResponse<bool>(false, false, $"Failed to update user \n {ex.InnerException.Message}"));
                    }

                }
            }
            else
            {
                return Json(new ApiResponse<bool>(false, false, "Please fill out all required fields."));
            }
        }

        [HttpDelete]
        [NoCache]
        [SessionExpire]
        public async Task<JsonResult> DeleteUser(int id)
        {
            if (id > 0)
            {
                try
                {
                    var user = await _userService.GetUserByIdAsync(id);
                    await _userService.DeleteUserAsync(user);
                    return Json(new ApiResponse<bool>(true, true, "A user was Successfully Deleted"));
                }
                catch (Exception ex)
                {
                    await _userService.RollbackTransaction();
                    return Json(new ApiResponse<bool>(false, false, $"Failed to delete user \n {ex.InnerException.Message}"));
                }

            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<IActionResult> GetRoles(string q)
        {
            List<RolesViewModel> vm = new List<RolesViewModel>();
            // Add parts to the list.
            var roles = await _roleService.GetAllRolesAsync();
            foreach (var role in roles)
            {
                vm.Add(new RolesViewModel { Id = role.Id, Name = role.Name });
            }
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                vm = vm.Where(x => x.Name.ToLower().StartsWith(q.ToLower())).ToList();
            }
            return Json(new { items = vm });
        }

        [HttpGet]
        [NoCache]
        [SessionExpire]
        public async Task<IActionResult> GetUserCardData()
        {
            const int page = 1;
            const int pageSize = 10;
            var users = await _userService.GetAllPagedUsersAsync(page, pageSize);
            var vm = _mapper.Map<List<UserViewModel>>(users);
            var pagedModel = new PagedViewModel<UserViewModel>
            {
                Model = vm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = await _userService.CountUserAsync()
            };
            return PartialView("~/Views/Setup/PartialViews/_UserCardPatialView.cshtml", pagedModel);
        }
        #endregion Users

        #region ROLES
        [NoCache]
        [SessionExpire]
        public IActionResult Roles()
        {
            return View();
        }
        #endregion ROLES

        #region PERMISSIONS
        [NoCache]
        [SessionExpire]
        public IActionResult Permissions()
        {
            return View();
        }
        #endregion PERMISSIONS

    }
}
