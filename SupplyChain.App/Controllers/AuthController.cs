using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(
            IUserService userService,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var userIsLoggedIn = CurrentUser.GetUserName();
            if (!string.IsNullOrEmpty(userIsLoggedIn))
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
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
                await CurrentUser.StartSession(_user, _userService);
                HttpContext.Session.SetString("userObj", $"{_user}");

                return RedirectToAction("Index", "Product");
            }
        }

        public IActionResult TimeOut()
        {
            double sessionTimeout = double.Parse(_configuration["SessionTimeOut"] ?? "20");
            ViewBag.SessionTimeout = sessionTimeout;
            return View();
        }

        public IActionResult Logout()
        {
            CurrentUser.Logout();
            return RedirectToAction("Login");
        }

    }
}
