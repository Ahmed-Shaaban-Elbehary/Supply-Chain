using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.App.App_Class;
using SupplyChain.App.ViewModels;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;

namespace SupplyChain.App.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserSerivce _session;
        private readonly IMapper _mapper;
        public LoginController(IUserService userService,
            ICurrentUserSerivce session,
            IMapper mapper)
        {
            _userService = userService;
            _session = session;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var errorMessage = TempData["ErrorMessage"] as string;
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        public IActionResult Supplier(string t)
        {
            if (t != "Supplier")
            {
                TempData["ErrorMessage"] = "You Are Not Supplier!";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Supplier(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "In-Corrent Username Or Password!";
                return View();
            }
            else
            {
                bool isValid = await _userService.ValidateUserCredentialsAsync(user.Email, user.Password);
                if (!isValid)
                {
                    ViewBag.ErrorMessage = "Invalid User, Try again with different Credential";
                    return View();
                }
                else
                {
                    var loggedInUser = await _userService.GetUserByEmailAsync(user.Email);
                    loggedInUser.IsSupplier = true;
                    var _user = _mapper.Map<User>(loggedInUser);
                    await _session.SetUserAsync(_user);
                }
            }
            return View();
        }

        public IActionResult Buyer(string t)
        {
            if (t != "Buyer")
            {
                TempData["ErrorMessage"] = "You Are Not Buyer!";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Buyer(LoginViewModel user)
        {
            return View();
        }
    }
}
