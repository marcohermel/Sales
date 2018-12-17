using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesMVC.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SalesMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SalesMVCContext _context;
        private  UserManager<UserSys> _userManager;
        private  SignInManager<UserSys> _signInManager;
        public AccountController(UserManager<UserSys> userManager, SignInManager<UserSys> signInManager, SalesMVCContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

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

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //model.Email = "admin@sellseverything.com";
               // model.Password = "admin123";
                UserSys usersys = new UserSys { Login = "Administrator", Email = model.Email, Password = model.Password };
              
                //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,false, lockoutOnFailure: false);
                var result = await _signInManager.PasswordSignInAsync(usersys, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                   
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }
    }
}