using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Models;

namespace Asm_Csharp4.IServices
{
    interface IProductService
    {
        List<Products> GetListProduct();
        List<Products> FindByName(string name);
        Products GetById(int? producId);
        void Save(Products product);
        void Update(Products products);
        int Delete(int productId);
        bool CheckIdProduct(int idProduct);

        public Products GetProductsObj(int idProducts);
    }
}
