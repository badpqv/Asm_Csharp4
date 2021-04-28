using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Services;
using System.Web;
using Asm_Csharp4.Models;
using Microsoft.AspNetCore.Http;

namespace Asm_Csharp4.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;
        private ICustomerService _iCustomerService;
        public bool logState { get; set; } = false;
        public AccountController(DatabaseContext context)
        {
            _context = context;
            _iCustomerService = new CustomerService(_context);
        }
        public IActionResult Login()
        {
            return View();

        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost,ActionName("Register")]
        public IActionResult ConfirmRegister([Bind("FullName", "Email", "SDT", "CmndCCCD", "Address", "Username", "Password", "Quyen")] Customers customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }
                if (customer.Id == 0)
                {
                    _iCustomerService.Save(customer);
                    TempData["Success"] = "<span class='alert alert-success'>Đăng kí thành công</span>";
                }
                return View();
            }
            catch (Exception e)
            {
                return View(customer);
            }
        }
        [HttpPost,ActionName("Login")]
        public IActionResult ConfirmLogin(string user,string password)
        {
            var usr = _iCustomerService.GetListCustomers().FirstOrDefault(c=>c.Username.ToLower().Equals(user.ToLower())&&c.Password == password);
            if (usr == null)
            {
                TempData["Error"] = "<span class='alert alert-danger'>Sai tên tài khoản hoặc mật khẩu</span>";
                return View();
            }
            HttpContext.Session.SetString("Login","Welcome, " + user);
            HttpContext.Session.SetString("Username",user);
            logState = true;
             return RedirectToAction("Index","Home");
        }
       
    }
}
