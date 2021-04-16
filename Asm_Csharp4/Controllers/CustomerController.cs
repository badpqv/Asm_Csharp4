using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;
using Asm_Csharp4.Services;

namespace Asm_Csharp4.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DatabaseContext _context;
        private ICustomerService _iCustomerService;

        public CustomerController(DatabaseContext context)
        {
            _context = context;
            _iCustomerService = new CustomerService(_context);
        }
        public IActionResult Index()
        {
            try
            {
                var lstCustomers = _iCustomerService.GetListCustomers();
                return View(lstCustomers);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var customer = _iCustomerService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName", "TaiKhoan", "MatKhau","Quyen")] Customers customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (customer.Id == 0)
                    {
                        _iCustomerService.Save(customer);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(customer);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _iCustomerService.GetCustomerObj(id);
            return View(customer);
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(Customers customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _iCustomerService.Update(customer);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(customer);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _iCustomerService.GetCustomerObj(id);
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_iCustomerService.CheckIdCustomer(id))
                    {
                        _iCustomerService.Delete(id);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
