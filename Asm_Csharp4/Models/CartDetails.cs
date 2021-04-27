using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm_Csharp4.Models
{
    [Table("Cart_details")]
    public partial class CartDetails
    {
        [Key]
        public int IdCart { get; set; }
        [Key]
        public int IdProduct { get; set; }
        public int? IdCustomer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BuyDate { get; set; }

        [ForeignKey(nameof(IdCart))]
        [InverseProperty(nameof(Carts.CartDetails))]
        public virtual Carts IdCartNavigation { get; set; }
        [ForeignKey(nameof(IdCustomer))]
        [InverseProperty(nameof(Customers.CartDetails))]
        public virtual Customers IdCustomerNavigation { get; set; }
        [ForeignKey(nameof(IdProduct))]
        [InverseProperty(nameof(Products.CartDetails))]
        public virtual Products IdProductNavigation { get; set; }
    }
}
