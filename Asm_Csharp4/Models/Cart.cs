using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Asm_Csharp4.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetails>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }
        [Display(Name = "Đơn giá")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Price { get; set; }
        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [InverseProperty("MaCartNavigation")]
        public virtual ICollection<CartDetails> CartDetails { get; set; }
    }
}
