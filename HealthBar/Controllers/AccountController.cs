using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using HealthBar.Domain;
using HealthBar.ViewModels;
using HealthBar.Services;
using Microsoft.AspNetCore.Identity;
using HealthBar.Models;

namespace HealthBar.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (!Utility.IsValidEmail(model.Email))
            {
                return RedirectToAction("Registration", "Account");
            }

            _userService.InsertUser(model.Username, model.Password, model.Email);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<ActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            string userRole = _userService.ValidateUser(model.Email, model.Password);

            if (userRole != null)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Email, model.Email),
                                               new Claim(ClaimTypes.Role, userRole)};

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties { };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect E-mail and/or Password. Please try again.");
                return View("Login", model);
            }
        }

        [HttpGet]
        public IActionResult UserAccount()
        {
            string email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

            User user = _userService.GetOne(email);

            return View(user);
        }

        [HttpGet]
        public IActionResult ModifyUser()
        {
            string email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

            User user = _userService.GetOne(email);
            ModifyViewModel modifyModel = new ModifyViewModel()
            {
                User = user,
                Modification = null
            };

            return View(modifyModel);
        }

        [HttpPost]
        public IActionResult ModifyUser([FromForm(Name = "Modification.Id")] int id,
            [FromForm(Name = "Modification.Username")] string username, [FromForm(Name = "Modification.Email")] string email,
            [FromForm(Name = "Modification.Password")] string password, [FromForm(Name = "Modification.PostalCode")] int postalCode,
            [FromForm(Name = "Modification.City")] string city, [FromForm(Name = "Modification.Address")] string address)
        {
            _userService.UpdateUser(id, username, password, email, postalCode, city, address);

            return RedirectToAction("LogOutAndPromoteLogin", "Account");
        }

        [HttpGet]
        public async Task<ActionResult> LogOutAndPromoteLoginAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}