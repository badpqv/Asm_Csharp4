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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm_Csharp4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _iProductService;
        private readonly DatabaseContext _context;
        public HomeController(ILogger<HomeController> logger,DatabaseContext context)
        {
            _context = context;
            _logger = logger;
            _iProductService = new ProductService(_context);
        }

        public IActionResult Index()
        {
            var lstProduct = _iProductService.GetListProduct();
            return View(lstProduct);
        }
     
        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.DanhMuc = new SelectList(_context.Categories, "Id", "Name");
            var product = _iProductService.GetById(id);
            return View(product);
        }


        
    }
}
