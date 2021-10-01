using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mobile_Brand.Models;
using Mobile_Brand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_Brand.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> userManager;
        public SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userManager.FindByNameAsync(model.Username);
                var singin = await signInManager.PasswordSignInAsync(result, model.Password, false, false);
                if (singin.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }
            
            ModelState.AddModelError("", "Invalid Username or Password");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
