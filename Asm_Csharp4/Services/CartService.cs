using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Context;
using Asm_Csharp4.IServices;
using Asm_Csharp4.Models;

namespace Asm_Csharp4.Services
{
    public class CartService : ICartService
    {
        private readonly DatabaseContext _context;
        private List<Cart> _lstCarts;
        public CartService(DatabaseContext context)
        {
            _context = context;
            _lstCarts = new List<Cart>();
            GetListCart();
        }

        public List<Cart> GetListCart()
        {
            _lstCarts = _context.Cart.ToList();
            return _lstCarts;
        }

        public bool FindExistProduct(string name)
        {
            return  _context.Cart.Any(c => c.ProductName.Equals(name));
        }

        public void AddCart(Cart cart)
        {
            _context.Cart.Add(cart);
            _context.SaveChangesAsync();
        }

        public void UpdateQuantity(Cart carts)
        {
            var cart = _context.Cart.FirstOrDefault(c=>c.ProductName == carts.ProductName);
            Console.WriteLine("ProName: " +cart.ProductName);
            cart.Quantity = carts.Quantity+1;
            _context.Cart.Update(cart);
            _context.SaveChanges();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
