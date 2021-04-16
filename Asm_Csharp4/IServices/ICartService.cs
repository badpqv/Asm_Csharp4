using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Asm_Csharp4.IServices
{
    interface ICartService
    {
        List<Cart> GetListCart();
        void AddCart(Cart cart);
        void UpdateQuantity(Cart cart);
        bool FindExistProduct(string name);
        int Delete(int id);

    }
}
