using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int Quantity { get; set; }
        public int IdCustomer { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [ForeignKey(nameof(IdCustomer))]
        [InverseProperty(nameof(Customers.Carts))]
        public virtual Customers IdCustomerNavigation { get; set; }
        [InverseProperty("IdCartNavigation")]
        public virtual ICollection<CartDetails> CartDetails { get; set; }
    }
}
