using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SalesMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SalesMVCContext _context;

        public AccountController(SalesMVCContext context)
        {
            _context = context;
        }

        public ClaimsIdentity UserIdentity { get; private set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            model.CheckEmailAndPassword = true;
            return View(model);
        }

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _context.UserSys.Include(u => u.Role).FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (result != null)
                {

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Role, result.Role.Name),
                    new Claim("UserId", result.Id.ToString())
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    Thread.CurrentPrincipal = claimsPrincipal;

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                        });


                    return RedirectToAction("Index", "Customers");

                }

            }
            ModelState.AddModelError("", "Invalid login attempt");
            model.CheckEmailAndPassword = false;
            return View(model);
        }


    }
}
