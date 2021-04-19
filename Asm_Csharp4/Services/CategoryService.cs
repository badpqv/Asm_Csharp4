using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;
using Microsoft.EntityFrameworkCore;

namespace Asm_Csharp4.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _context;
        private List<Categories> _lstCategories;

        public CategoryService(DatabaseContext context)
        {
            _lstCategories = new List<Categories>();
            _context = context;
            GetListCategories();
        }
        public List<Categories> GetListCategories()
        {
            _lstCategories = _context.Categories.ToList();
            return _lstCategories;
        }

        public Categories GetById(int? categoryId)
        {
            return _context.Categories.SingleOrDefault(c => c.Id == categoryId);
        }

        public bool CheckIdCategory(int idCategory)
        {
            return _lstCategories.Any(c => c.Id == idCategory);
        }

        public Categories GetCategoryObj(int idCategory)
        {
            var data = _lstCategories.FirstOrDefault(c => c.Id == idCategory);
            return data;
        }

        public void Save(Categories category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Categories category)
        {
            var ctg = _context.Categories.Find(category.Id);
            ctg.Name = category.Name;
            _context.Categories.Update(ctg);
            _context.SaveChanges();
        }

        public int Delete(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            try
            {
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    return 0;
                }
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            return -1;
        }
    }
}
