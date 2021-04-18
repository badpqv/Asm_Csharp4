using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;
using Asm_Csharp4.Services;
using Microsoft.EntityFrameworkCore;

namespace Asm_Csharp4.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DatabaseContext _context;
        private ICategoryService _iCategoryService;

        public CategoryController(DatabaseContext context)
        {
            _context = context;
            _iCategoryService = new CategoryService(_context);
        }
        public IActionResult Index()
        {
            try
            {
                var lstCategories = _iCategoryService.GetListCategories();
                return View(lstCategories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var category = _iCategoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Categories category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (category.Id == 0)
                    {
                        _iCategoryService.Save(category);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(category);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _iCategoryService.GetById(id);
            return View(category);
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(Categories category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _iCategoryService.Update(category);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(category);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _iCategoryService.GetCategoryObj(id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_iCategoryService.CheckIdCategory(id))
                    {
                        _iCategoryService.Delete(id);
                    }
                }
                Console.WriteLine(_iCategoryService.Delete(id));
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
    }
}
