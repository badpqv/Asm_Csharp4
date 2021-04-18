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
        [Display(Name = "Tên sản phẩm")]
        [StringLength(200)]
        public string ProductName { get; set; }
        [Display(Name = "Đơn giá ")]
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [InverseProperty("IdCartNavigation")]
        public virtual ICollection<CartDetails> CartDetails { get; set; }
    }
}
