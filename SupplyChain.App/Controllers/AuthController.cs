﻿using AutoMapper;
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
        public AuthController(
            IUserService userService,
            IConfiguration configuration,
            IHubContext<NotificationHub> notificationHubContext,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
            _notificationHubContext = notificationHubContext;
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
                    // Serialize the user object to JSON and store it in the session
                    var userJson = JsonConvert.SerializeObject(_user); // You'll need to add a reference to Newtonsoft.Json
                    HttpContext.Session.SetString("User", userJson);
                    //await CurrentUser.StartSession(_user, _userService);
                    HttpContext.Session.SetString("userObj", $"{_user}");
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
        public IActionResult TimeOut()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            CurrentUser.Logout();
            return RedirectToAction("Login");
        }

    }
}
