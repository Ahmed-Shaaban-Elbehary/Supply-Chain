using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SupplyChain.App.Notification;
using SupplyChain.App.Utils.Validations;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IUserSessionService _userSessionService;
        public AuthController(
            IUserService userService,
            IConfiguration configuration,
            IHubContext<NotificationHub> notificationHubContext,
            IUserSessionService userSessionService,
            IMapper mapper) : base(userSessionService)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
            _notificationHubContext = notificationHubContext;
            _userSessionService = userSessionService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = "Invalid Username Or Password!";
                    return View();
                }
                bool isValid = await _userService.ValidateUserCredentialsAsync(user.Email, user.Password);
                if (!isValid)
                {
                    ViewBag.ErrorMessage = "Invalid User, Try again with different Credential";
                    return View();
                }
                else
                {
                    bool ImSupplier = user.IsSupplier; //I supplier checked.
                    var loggedInUser = await _userService.GetUserByEmailAsync(user.Email);

                    if (loggedInUser.IsSupplier == true && ImSupplier != true)
                    {
                        ViewBag.ErrorMessage = "You are supplier, please login as a supplier!";
                        return View();
                    }
                    var _user = _mapper.Map<User>(loggedInUser);
                    var roles = await _userService.GetUserRolesAsync(_user.Id);
                    if (roles.Any())
                    {
                        await _userSessionService.SetLoggedInUserRoles(roles.ToList());
                    }
                    var permissions = await _userService.GetUserPermissionsAsync(_user.Id);
                    if (permissions.Any())
                    {
                        await _userSessionService.SetLoggedInUserPermissions(permissions.ToList());
                    }
                    //set user 
                    await _userSessionService.SetUserAsync(_user);
                    return RedirectToAction("Index", "Product");
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ErrorResponse.PreException(ex);
                var errorResponseJson = JsonConvert.SerializeObject(errorResponse);
                TempData["ErrorResponse"] = errorResponseJson;
                await _userService.RollbackTransaction();
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> TimeOut()
        {
            await _userSessionService.ClearUserSessionAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userSessionService.ClearUserSessionAsync();
            return RedirectToAction("Login");
        }

    }
}
