using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using NuGet.Packaging;
using SupplyChain.App.Utils.Validations;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;
using System.ComponentModel;
using System.Net;

namespace SupplyChain.App.Controllers
{
    public class SetupController : Controller
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
        public IActionResult Index()
        {
            return View();
        }

        #region Category
        [HttpGet]
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
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddEditCategory(ProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<ProductCategory>(vm);

                if (category.Id == 0) // Adding a new category
                {
                    await _productCategoryService.CreateProductCategoryAsync(category);
                    return Json(new { message = "Add New Category Successed!" });
                }
                else // Editing an existing category
                {
                    await _productCategoryService.UpdateProductCategoryAsync(category);
                    return Json(new { message = "Edit Category Successed!" });
                }
            }
            return Json(new { message = "Oops, Error Occurred, Please Try Again!" });
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteCategory(int id)
        {
            if (id > 0)
            {
                var productCategory = await _productCategoryService.GetProductCategoryByIdAsync(id);
                await _productCategoryService.DeleteProductCategoryAsync(productCategory);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion Category

        #region Manufacturer
        [HttpGet]
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
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddEditManufacturer(ManufacturerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var manufacturer = _mapper.Map<Manufacturer>(vm);

                if (manufacturer.Id == 0) // Adding a new category
                {
                    await _manufacturerService.CreateManufacturerAsync(manufacturer);
                    return Json(new { message = "Add New Manufacturer Successed!" });
                }
                else // Editing an existing category
                {
                    await _manufacturerService.UpdateManufacturerAsync(manufacturer);
                    return Json(new { message = "Edit Manufacturer Successed!" });
                }

            }

            // If the model is not valid, redisplay the form with validation errors
            //ViewBag.Manufacturers = await _manufacturerService.GetAllManufacturerAsync();
            return Json(new { message = "Oops, Error Occurred, Please Try Again!" });
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteManufacturer(int id)
        {
            if (id > 0)
            {
                var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
                await _manufacturerService.DeleteManufacturerAsync(manufacturer);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion Manufacturer

        #region Users

        [HttpGet]
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

        #endregion Users

        #region ROLES
        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult UserRoles()
        {
            return View();
        }
        #endregion ROLES

        #region PERMISSIONS
        public IActionResult Permissions()
        {
            return View();
        }
        public IActionResult RolePermissions()
        {
            return View();
        }
        #endregion PERMISSIONS

    }
}
