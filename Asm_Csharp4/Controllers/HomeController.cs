using Asm_Csharp4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm_Csharp4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _iProductService;
        private ICartService _iCartService;
        private readonly DatabaseContext _context;
        public HomeController(ILogger<HomeController> logger,DatabaseContext context)
        {
            _context = context;
            _logger = logger;
            _iProductService = new ProductService(_context);
            _iCartService = new CartService(_context);
        }

        public IActionResult Index()
        {
             

            var lstProduct = _iProductService.GetListProduct();
            return View(lstProduct);
        }
        [HttpGet,ActionName(nameof(Index))]
        public async Task<IActionResult> Search(string name, int categoryId)
        {
            try
            {
                ViewBag.DanhMuc = new SelectList(_context.Categories, "Id", "Name");
                var products = _iProductService.GetListProduct();
                if (name == null)
                {
                    name = "";
                }
                products = categoryId != 0 ? products.Where(c => c.Name.ToLower().Contains(name.ToLower()) && c.CategoryId == categoryId).ToList() : products.Where(c => c.Name.Contains(name)).ToList();
                var count = products.Count;
                TempData["Result"] = $"<h1 class='display-6 text-center text-dark'>Tìm thấy <span class='text-info'>{count}</span> kết quả</h1>";
                return View(products);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
             
            ViewBag.DanhMuc = new SelectList(_context.Categories, "Id", "Name");
            var product = _iProductService.GetById(id);
            return View(product);
        }
      
        public IActionResult Buy(string name, decimal price)
        {
            Carts carts;
            var userName = HttpContext.Session.GetString("Username");
            var idCust = _context.Customers.FirstOrDefault(c => c.Username == userName).Id;
            if (_iCartService.GetListCart(userName).Count == 0)
            {
                carts = new Carts { ProductName = name, Price = price, Quantity = 1, IdCustomer = idCust };
                _iCartService.AddCart(carts);
            }
            else
            {
                var soLuong = _iCartService.GetCurrentQuantity(name, userName);
                var exist = _iCartService.FindExistProduct(name);
                if (exist)
                {
                    carts = new Carts { ProductName = name, Price = price, Quantity = soLuong, IdCustomer = idCust };
                    carts.Quantity++;
                    _iCartService.UpdateQuantity(carts);
                }
                else
                {
                    carts = new Carts() { ProductName = name, Price = price, Quantity = 1, IdCustomer = idCust };
                    _iCartService.AddCart(carts);
                }
            }
            return RedirectToAction("Details");
        }

    }
}
