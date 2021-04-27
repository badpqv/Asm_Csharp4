using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            var userName = HttpContext.Session.GetString("Username");
            var idCust = _context.Customers.FirstOrDefault(c => c.Username == userName).Id;
            var cart = _iCartService.GetListCart(idCust);
            
            ViewBag.total = cart.Sum(c => c.Price * c.Quantity);
            return View(cart);
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

   
        public IActionResult Buy(string name, decimal price)
        {
            Carts carts;
            var userName = HttpContext.Session.GetString("Username");
            var idCust = _context.Customers.FirstOrDefault(c => c.Username == userName).Id;
            if (_iCartService.GetListCart(idCust).Count == 0)
            {
                carts = new Carts { ProductName = name, Price = price, Quantity = 1, IdCustomer = idCust };
                _iCartService.AddCart(carts);
                TempData["Added"] = "<span class='alert alert-success position-fixed bottom 0 end-0 p-3'>Đã thêm 1 sản phẩm @Model.Name vào giỏ hàng</span>";
            }
            else
            {
                var soLuong = _iCartService.GetCurrentQuantity(name, userName);
                var exist = _iCartService.FindExistProduct(name,idCust);
                if (exist)
                {
                    carts = new Carts { ProductName = name, Price = price, Quantity = soLuong, IdCustomer = idCust };
                    carts.Quantity++;
                    _iCartService.UpdateQuantity(carts);
                    TempData["Added"] = "<span class='alert alert-success position-fixed bottom 0 end-0 p-3'>Đã thêm 1 sản phẩm @Model.Name vào giỏ hàng</span>";
                }
                else
                {
                    carts = new Carts() { ProductName = name, Price = price, Quantity = 1, IdCustomer = idCust };
                    _iCartService.AddCart(carts);
                    TempData["Added"] = "<span class='alert alert-success position-fixed bottom 0 end-0 p-3'>Đã thêm 1 sản phẩm @Model.Name vào giỏ hàng</span>";
                }
            }

            return RedirectToAction("Details","Home");
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
