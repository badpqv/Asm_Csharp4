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
        //Lấy danh sách hoá đơn
        List<Carts> GetListCart();
        List<Carts> GetListCart(string userName);
        //Kiểm tra IdCart để thực hiện xoá
        bool CheckCartId(int id);
        //Kiểm tra sản phẩm có tồn tại trong giỏ hàng ko?
        bool FindExistProduct(string name);
        //Lấy số lượng sản phẩm hiện tại
        int? GetCurrentQuantity(string name,string userName);
        //Chức năng
        void AddCart(Carts cart);
        void UpdateQuantity(Carts cart);
        string Delete(int id);
    }
}
