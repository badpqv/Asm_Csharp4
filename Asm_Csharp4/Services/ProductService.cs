using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;
using Microsoft.EntityFrameworkCore;

namespace Asm_Csharp4.Services
{
    public class ProductService:IProductService
    {
        private readonly DatabaseContext _context;
        private List<Products> lstProducts;

        public ProductService(DatabaseContext context)
        {
            lstProducts = new List<Products>();
            _context = context;
            GetListProduct();
        }


     
        public List<Products> GetListProduct()
        {
            lstProducts = _context.Products.ToList();
            return lstProducts;
        }

        public List<Products> FindByName(string name)
        {
            var productsList = _context.Products.Where(c => c.Name.Contains(name)).ToList();
            Console.WriteLine(_context.Products.Where(c => c.Name.Contains(name)).ToString());
            return productsList;
        }

        public Products GetById(int? productId)
        {
            return _context.Products.SingleOrDefault(c => c.Id == productId);
        }

        public bool CheckIdProduct(int idProduct)
        {
            return lstProducts.Any(s => s.Id == idProduct);
        }

        public Products GetProductsObj(int idProduct)
        {
            var data = lstProducts.FirstOrDefault(c => c.Id == idProduct);
            Console.WriteLine("Id: "+idProduct);
            Console.WriteLine("Data Id: " + data.Id.ToString());
            return data ;
        }

        public void Save(Products products)
        {
            
            _context.Products.Add(products);
            _context.SaveChanges();
        }

        public void Update(Products products)
        {
            var product = _context.Products.Find(products.Id);
            product.Name = products.Name;
            product.Description = products.Description;
            product.Price = products.Price;
            product.Image = products.Image;
            product.CategoryId = products.CategoryId;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public int Delete(int idProduct)
        {
            var product = _context.Products.FirstOrDefault(c => c.Id == idProduct);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return 0;
            }

            return 1;
        }
    }
}
