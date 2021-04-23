using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Asm_Csharp4.Services
{
    public class CartService : ICartService
    {
        private readonly DatabaseContext _context;
        private List<Carts> _lstCarts;
        CartDetails detail;
        public CartService(DatabaseContext context)
        {
            _context = context;
            _lstCarts = new List<Carts>();
            GetListCart();
        }
        public List<Carts> GetListCart(string userName)
        {
            var idCust = _context.Customers.FirstOrDefault(c => c.Username == userName).Id;
            _lstCarts = _context.Carts.Where(c=>c.IdCustomer == idCust).ToList();
            return _lstCarts;
        }      
        public List<Carts> GetListCart()
        {
            _lstCarts = _context.Carts.ToList();
            return _lstCarts;
        }
        public bool FindExistProduct(string name)
        {
            return  _context.Carts.Any(c => c.ProductName.Equals(name));
        }
        public  void AddCart(Carts cart)
        {
            var idProduct = _context.Products.FirstOrDefault(c => c.Name == cart.ProductName).Id;
            detail = new CartDetails() {IdCart = cart.Id, IdProduct = idProduct,IdCustomer = cart.IdCustomer,BuyDate = DateTime.Now, IdCartNavigation = cart};
            _context.CartDetails.Add(detail);
            _context.SaveChanges();
        }
        public void UpdateQuantity(Carts carts)
        {
            var cart = _context.Carts.FirstOrDefault(c=>c.ProductName == carts.ProductName && c.IdCustomer == carts.IdCustomer);
            cart.Quantity = carts.Quantity;
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }
        public int? GetCurrentQuantity(string name,string userName)
        {
            var idCust = _context.Customers.FirstOrDefault(c => c.Username == userName).Id;
            var cart = _context.Carts.Where(c => c.ProductName == name && c.IdCustomer == idCust).Select(c => c.Quantity).FirstOrDefault();
            return cart;
        }

        public bool CheckCartId(int id)
        {
            return _context.Carts.Any(c => c.Id == id);
        }

        public string Delete(int id)
        {
                var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                _context.SaveChanges();
                return "Xoá thành công";
            }

            return "Xoá thất bại";
        }
    }
}
