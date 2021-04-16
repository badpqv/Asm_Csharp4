using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Models;

namespace Asm_Csharp4.IServices
{
    interface ICategoryService
    {
        List<Categories> GetListCategories();
        Categories GetById(int? categoryId);
        void Save(Categories category);
        void Update(Categories category);
        int Delete(int categoryId);
        bool CheckIdCategory(int idCategory);

        public Categories GetCategoryObj(int idCategory);
    }
}
