using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asm_Csharp4.Models;

namespace Asm_Csharp4.IServices
{
    interface ICustomerService
    {
        List<Customers> GetListCustomers();
        Customers GetById(int? customerId);
        void Save(Customers customer);
        void Update(Customers customer);
        int Delete(int customerId);
        bool CheckIdCustomer(int idCustomer);

        public Customers GetCustomerObj(int idCustomer);
    }
}
