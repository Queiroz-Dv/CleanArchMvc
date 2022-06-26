﻿using CleanArchMvc.Domain.Account;
using CleanArchMvc.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchMvc.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authetication;

        public AccountController(IAuthenticate authenticate)
        {
            _authetication = authenticate;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authetication.Authenticate(model.Email, model.Password);

            if (result)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                    return RedirectToAction("Index", "Home");

                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.(Password must be strong).");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _authetication.RegisterUser(model.Email, model.Password);

            if (result)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be strong).");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _authetication.Logout();
            return Redirect("/Account/Login");
        }
    }
}
