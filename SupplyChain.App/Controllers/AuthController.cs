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
                    ViewBag.ErrorMessage = "Invalid Username or Password!";
                    return View();
                }
                bool isValid = await _userService.ValidateUserCredentialsAsync(user.Email, user.Password);
                if (!isValid)
                {
                    ViewBag.ErrorMessage = "Invalid User, try again with different credentials";
                    return View();
                }
                else
                {
                    bool IsSupplier = user.IsSupplier; // Check if the user is a supplier.
                    var loggedInUser = await _userService.GetUserByEmailAsync(user.Email);

                    if (loggedInUser.IsSupplier && !IsSupplier)
                    {
                        ViewBag.ErrorMessage = "You are a supplier, please log in as a supplier!";
                        return View();
                    }

                    // Create a unique session token (you should implement this part)
                    string userSessionToken = GenerateUserSessionToken();

                    var _user = _mapper.Map<User>(loggedInUser);
                    var roles = await _userService.GetUserRolesAsync(_user.Id);
                    var permissions = await _userService.GetUserPermissionsAsync(_user.Id);

                    // Store user-related data in the session token (e.g., in a cookie)
                    SetUserSessionData(userSessionToken, _user, roles.ToList(), permissions.ToList());

                    // Redirect the user to the desired page
                    return RedirectToAction("Index", "Product");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and redirect to an error page
                var errorResponse = ErrorResponse.PreException(ex);
                var errorResponseJson = JsonConvert.SerializeObject(errorResponse);
                TempData["ErrorResponse"] = errorResponseJson;
                await _userService.RollbackTransaction();
                return RedirectToAction("Index", "Error");
            }
        }

        // Generate a unique user session token (you should implement this part)
        private string GenerateUserSessionToken()
        {
            // Generate a unique token, e.g., a GUID
            return Guid.NewGuid().ToString();
        }

        // Store user-related data in the session token (e.g., in a cookie)
        private void SetUserSessionData(string userSessionToken, User user, List<string> roles, List<string> permissions)
        {
            // Set user-specific session data in a cookie (you need to implement this)
            Response.Cookies.Append("UserSessionToken", userSessionToken);

            // You can store user-related data in the cookie or other session storage mechanisms.
            // For example:
            Response.Cookies.Append("UserId", user.Id.ToString());
            Response.Cookies.Append("UserRoles", string.Join(",", roles));
            Response.Cookies.Append("UserPermissions", string.Join(",", permissions));
        }

        [HttpGet]
        public IActionResult TimeOut()
        {
            _userSessionService.ClearUserSession();
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _userSessionService.ClearUserSession();
            return RedirectToAction("Login");
        }

    }
}
