using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Asm_Csharp4.Models
{
    public partial class Products
    {
        public Products()
        {
            CartDetails = new HashSet<CartDetails>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [StringLength(200)]
        [Display(Name = "Mô tả sản phẩm")]
        public string Description { get; set; }
        [Display(Name = "Đơn giá")]
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [StringLength(100)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Display(Name = "Danh mục")]
        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Categories.Products))]
        public virtual Categories Category { get; set; }
        [InverseProperty("IdProductNavigation")]
        public virtual ICollection<CartDetails> CartDetails { get; set; }
    }
}
