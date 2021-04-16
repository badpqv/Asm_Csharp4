using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;
using Asm_Csharp4.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm_Csharp4.Controllers
{
    public class ProductController : Controller
    {
        private readonly DatabaseContext _context;
        private IProductService _iProductService;
        public ProductController(DatabaseContext context)
        {
            _context = context;
            _iProductService = new ProductService(_context);
        }
        public IActionResult Index()
        {
            try
            {
                var lstProducts = _iProductService.GetListProduct();
                return View(lstProducts);
            }
            catch (Exception e)
            {
                return View();
            }
        }

      
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var lstDanhMuc = new SelectList(_context.Categories, "Id", "Name");
            ViewData["DanhMuc"] = lstDanhMuc;
            var product = _iProductService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
       
        [HttpGet]
        public IActionResult Create()
        {
            var lstDanhMuc = new SelectList(_context.Categories, "Id", "Name");
            ViewData["DanhMuc"] = lstDanhMuc;
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "Description", "Price", "Image","CategoryId")] Products product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (product.Id == 0)
                    {
                        _iProductService.Save(product);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewData["Error"] = "<script>alert('Thêm sản phẩm thất bại!')</script>";
                return View(product);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["DanhMuc"]= new SelectList(_context.Categories, "Id", "Name");
            
            var product = _iProductService.GetProductsObj(id);
            return View(product);
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(Products product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _iProductService.Update(product);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(product);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _iProductService.GetProductsObj(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_iProductService.CheckIdProduct(id))
                    {
                        _iProductService.Delete(id);
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
