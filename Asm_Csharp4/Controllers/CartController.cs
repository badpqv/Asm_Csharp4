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
    public class CartController : Controller
    {
        private readonly DatabaseContext _context;
        private ICartService _iCartService;
        private IProductService _iProductService;
        public CartController(DatabaseContext context)
        {
            _context = context;
            _iCartService = new CartService(_context);
            _iProductService = new ProductService(_context);
        }
        public IActionResult Index()
        {
            try
            {
                var cart = _iCartService.GetListCart();
                ViewBag.total = cart.Sum(c => c.Price * c.Quantity);
                return View(cart);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [NonAction]
        public int IsExist(string name)
        {
            try
            {
                var cart = _iCartService.GetListCart();
                for (var i = 0; i < cart.Count; i++)
                {
                    if (_iCartService.FindExistProduct(name))
                    {
                        return i;
                    }
                }

                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }
        public IActionResult Delete(int id)
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
                ;
            }
        }
        public IActionResult Buy(string name,decimal price,Cart carts)
        {
            if (_iCartService.GetListCart().Count == 0)
            {
                carts = new Cart {ProductName = name, Price = price, Quantity = 1};
                _iCartService.AddCart(carts);
            }
            else
            {
                var soLuong = _iCartService.GetCurrentQuantity(name);
                int index = IsExist(name);
                if (index != -1)
                {
                    carts = new Cart {ProductName = name, Price = price, Quantity = soLuong};
                    carts.Quantity++;
                _iCartService.UpdateQuantity(carts);
                }
                else
                {
                    carts = new Cart {ProductName = name, Price = price, Quantity = 1};
                    _iCartService.AddCart(carts);
                }
            }
            return RedirectToAction("Index");
        }


    }
}
