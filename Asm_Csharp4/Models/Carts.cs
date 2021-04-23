using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Asm_Csharp4.Models
{
    public partial class Carts
    {
        public Carts()
        {
            CartDetails = new HashSet<CartDetails>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
        [Column(TypeName = "money")]
        [Display(Name = "Đơn giá")]
        public decimal? Price { get; set; }
        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }
        public int? IdCustomer { get; set; }

        [ForeignKey(nameof(IdCustomer))]
        [InverseProperty(nameof(Customers.Carts))]
        public virtual Customers IdCustomerNavigation { get; set; }
        [InverseProperty("IdCartNavigation")]
        public virtual ICollection<CartDetails> CartDetails { get; set; }
    }
}
