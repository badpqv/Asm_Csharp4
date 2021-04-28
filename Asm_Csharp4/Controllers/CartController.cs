using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;
using Asm_Csharp4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Asm_Csharp4.Controllers
{
    public class CartController : Controller
    {
        private readonly DatabaseContext _context;
        private ICartService _iCartService;

        public CartController(DatabaseContext context)
        {
            _context = context;
            _iCartService = new CartService(_context);
        }
        public IActionResult Index()
        {
            try
            {
                var userName = HttpContext.Session.GetString("Username");
                var idCust = _context.Customers.FirstOrDefault(c => c.Username == userName).Id;
                var cust = _context.Customers.FirstOrDefault(c => c.Id == idCust);
                var cart = _iCartService.GetListCart(idCust);
                var sum = cart.Sum(c => c.Price * c.Quantity);
                ViewBag.total = String.Format(new CultureInfo("is-IS"), "{0:n0}", sum);
                ViewBag.customer = cust;
                return View(cart);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        public IActionResult Remove(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_iCartService.CheckCartId(id))
                    {
                        _iCartService.Delete(id);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {

                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));

            }
        }
        [HttpPost]
        public IActionResult UpdateCart(int id, int quantity)
        {
            if (ModelState.IsValid)
            {
                var cart = _iCartService.GetListCart().FirstOrDefault(c => c.Id == id);
                if (cart == null) return RedirectToAction(nameof(Index));
                cart.Quantity = quantity;
                _iCartService.UpdateQuantity(cart);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Buy(string name, decimal price,string image)
        {
            Carts carts;
            var userName = HttpContext.Session.GetString("Username");
            var idCust = _context.Customers.FirstOrDefault(c => c.Username == userName).Id;
            if (_iCartService.GetListCart(idCust).Count == 0)
            {
                carts = new Carts { ProductName = name, Price = price, Quantity = 1, IdCustomer = idCust,Image = image};
                _iCartService.AddCart(carts);
            }
            else
            {
                var soLuong = _iCartService.GetCurrentQuantity(name, userName);
                var exist = _iCartService.FindExistProduct(name, idCust);
                if (exist)
                {
                    carts = new Carts { ProductName = name, Price = price, Quantity = soLuong, IdCustomer = idCust,Image = image};
                    carts.Quantity++;
                    _iCartService.UpdateQuantity(carts);
                }
                else
                {
                    carts = new Carts() { ProductName = name, Price = price, Quantity = 1, IdCustomer = idCust,Image = image};
                    _iCartService.AddCart(carts);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var userName = HttpContext.Session.GetString("Username");
            var idCust = _context.Customers.FirstOrDefault(c => c.Username == userName).Id;
            var cart = _iCartService.GetListCart(idCust);
            ViewBag.total = cart.Sum(c => c.Price * c.Quantity);
            return View();
        }


        public async Task<IActionResult> ConfirmCheckout()
        {
            var userName = HttpContext.Session.GetString("Username");
            var idCust = _context.Customers.FirstOrDefault(c => c.Username == userName).Id;
            var carts = _iCartService.GetListCart(idCust);
            foreach (var item in carts)
            {
                _context.Carts.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
