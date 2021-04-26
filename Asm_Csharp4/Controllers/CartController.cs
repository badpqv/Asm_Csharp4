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
            var cart = _iCartService.GetListCart(userName);
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


        public IActionResult Checkout()
        {
            var userName = HttpContext.Session.GetString("Username");

            var cart = _iCartService.GetListCart(userName);
            ViewBag.total = cart.Sum(c => c.Price * c.Quantity);
            return View();
        }


        public async Task<IActionResult> ConfirmCheckout()
        {
            var userName = HttpContext.Session.GetString("Username");
            var carts = _iCartService.GetListCart(userName);
            foreach (var item in carts)
            {
                _context.Carts.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
