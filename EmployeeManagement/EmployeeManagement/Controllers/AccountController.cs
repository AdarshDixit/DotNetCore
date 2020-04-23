using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.ContactNumber
                };
                var sta1 = await this.userManager.CreateAsync(user, model.Password);
                if (sta1.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, false);
                    return RedirectToAction("GetEmployees", "Employees");
                }
                else
                {
                    foreach (var error in sta1.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    
                }
                
            }
           
            return View();
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
            
                var sta1 = await this.signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (sta1.Succeeded)
                { return RedirectToAction("GetEmployees", "Employees"); }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login");
                }
            }

            return View();
        }

        [Route("Logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            this.signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}