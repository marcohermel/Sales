using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesMVC.Models;

namespace SalesMVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly SalesMVCContext _context;

        public CustomersController(SalesMVCContext context)
        {
            _context = context;
        }
        public void LoadViewBags()
        {
            ViewBag.Genders = _context.Gender;
            ViewBag.Cities = _context.City;
            ViewBag.Regions = _context.Region;
            ViewBag.Classifications = _context.Classification;
            ViewBag.UsersSys = _context.UserSys;

            if (User.IsInRole(""))
            {

            }
            
        }
        // GET: Customers
        public async Task<IActionResult> Index()
        {

            CustomerViewModel ViewModel = new CustomerViewModel();
            ViewModel.Customers = await _context.VwCustomer.ToListAsync();

            LoadViewBags();
            return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(CustomerViewModel ViewModel)
        {
            ViewModel.Customers = await Filter(ViewModel);
            LoadViewBags();
            return View(ViewModel);
        }

        public async Task<IEnumerable<vwCustomer>> Filter(CustomerViewModel ViewModel)
        {
            return await _context.VwCustomer
                .Where(c => c.Name.Contains(ViewModel.Name) || String.IsNullOrEmpty(ViewModel.Name))
                .Where(c => c.GenderId == ViewModel.GenderId || ViewModel.GenderId == null)
                .Where(c => c.CityId == ViewModel.CityId || ViewModel.CityId == null)
                .Where(c => c.RegionId == ViewModel.RegionId || ViewModel.RegionId == null)
                .Where(c => c.ClassificationId == ViewModel.ClassificationId || ViewModel.ClassificationId == null)
                .Where(c => c.LastPurchase >= ViewModel.DateStart || ViewModel.DateStart == null)
                .Where(c => c.LastPurchase <= ViewModel.DateFinish || ViewModel.DateFinish == null)
                .ToListAsync();
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,GenderId,CityId,RegionId,LastPurchase,ClassificationId,UserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,GenderId,CityId,RegionId,LastPurchase,ClassificationId,UserId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
