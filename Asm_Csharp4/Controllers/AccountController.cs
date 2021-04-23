using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Services;
using System.Web;
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
        public IActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Login(string user,string password)
        {
            var usr = _iCustomerService.GetListCustomers().FirstOrDefault(c=>c.Username==user&&c.Password == password);
            if (usr == null)
            {
                TempData["Error"] = "Sai tên tài khoản hoặc mật khẩu";
                return RedirectToAction("Index");
            }
            HttpContext.Session.SetString("Login","Welcome, " + user);
            HttpContext.Session.SetString("Username",user);
            logState = true;
             return RedirectToAction("Index","Home");
        }

        public IActionResult Logout()
        {
         
            return RedirectToAction("Index", "Home");
        }
    }
}
