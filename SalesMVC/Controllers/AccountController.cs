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

        public AccountController(SalesMVCContext context)
        {
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

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.UserSys.Where(u => u.Login == model.Email && u.Password == model.Password).ToListAsync();

                if (result.Count > 0)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {

                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Customers");
                    }
                }

            }
            ModelState.AddModelError("", "Invalid login attempt");
            model.CheckEmailAndPassword = false;
            return View(model);
        }

    }
}