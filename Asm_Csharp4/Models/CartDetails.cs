using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Asm_Csharp4.Models
{
    [Table("Cart_details")]
    public partial class CartDetails
    {
        [Key]
        public int MaSp { get; set; }
        [Key]
        public int MaCart { get; set; }

        [ForeignKey(nameof(MaCart))]
        [InverseProperty(nameof(Cart.CartDetails))]
        public virtual Cart MaCartNavigation { get; set; }
        [ForeignKey(nameof(MaSp))]
        [InverseProperty(nameof(Products.CartDetails))]
        public virtual Products MaSpNavigation { get; set; }
    }
}
