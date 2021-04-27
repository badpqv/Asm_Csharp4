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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                ViewBag.DanhMuc = new SelectList(_context.Categories, "Id", "Name");
                var lstProducts = _iProductService.GetListProduct();
                return View(lstProducts);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpGet, ActionName(nameof(Index))]
        public IActionResult Search(string name, int categoryId)
        {
            ViewBag.DanhMuc = new SelectList(_context.Categories, "Id", "Name");
            var products = _iProductService.GetListProduct().OrderBy(c => c.CategoryId).ToList();
            if (name == null)
            {
                name = "";
            }
            products = categoryId != 0 ? products.Where(c => c.Name.Contains(name) && c.CategoryId == categoryId).ToList() : products.Where(c => c.Name.Contains(name)).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var lstDanhMuc = new SelectList(_context.Categories, "Id", "Name");
            ViewData["DanhMuc"] = lstDanhMuc;
            return View();
        }
        [HttpPost, ActionName(nameof(Create))]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct([Bind("Name", "Description", "Price", "Image", "CategoryId", "isDiscount", "Discount")] Products product)
        {
            try
            {
                var lstDanhMuc = new SelectList(_context.Categories, "Id", "Name");
                ViewData["DanhMuc"] = lstDanhMuc;
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (product.Id == 0)
                {
                    _iProductService.Save(product);

                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                return View(product);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            ViewData["DanhMuc"] = new SelectList(_context.Categories, "Id", "Name");

            var product = _iProductService.GetProductsObj(id);
            return View(product);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Products product)
        {
            try
            {
                ViewData["DanhMuc"] = new SelectList(_context.Categories, "Id", "Name");
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                _iProductService.Update(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ViewData["Error"] = "<script>alert('Sửa sản phẩm thất bại!')</script>";
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
        public IActionResult DeleteProduct(int id)
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
                Console.WriteLine(e.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
