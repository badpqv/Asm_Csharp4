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

        public int isExist(string name)
        {
            try
            {
                var cart = _iCartService.GetListCart();
                for (int i = 0; i < cart.Count; i++)
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

        public IActionResult Buy(string name,decimal price,Cart carts)
        {
            Console.WriteLine("Name: "+name);
            Console.WriteLine("Price: "+price);
            if (_iCartService.GetListCart().Count == 0)
            {
                carts = new Cart();
                carts.ProductName = name;
                carts.Price = price;
                carts.Quantity = 1;
                _iCartService.AddCart(carts);
            }
            else
            {
                var cart = _iCartService.GetListCart();
                int index = isExist(name);
                if (index != -1)
                {
                    carts = new Cart();
                    carts.ProductName = name;
                    carts.Price = price;
                    carts.Quantity = cart[index].Quantity;
                   _iCartService.UpdateQuantity(carts);
                }
                else
                {
                    carts = new Cart();
                    carts.ProductName = name;
                    carts.Price = price;
                    carts.Quantity = 1;
                    _iCartService.AddCart(carts);
                }
            }
            return RedirectToAction("Index");
        }


    }
}
