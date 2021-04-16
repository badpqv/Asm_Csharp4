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
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
      [Display(Name = "Mô tả")]
        [StringLength(200)]
        public string Description { get; set; }
        [Display(Name = "Đơn giá")]
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [Display(Name = "Hình ảnh")]
        [StringLength(100)]
        public string Image { get; set; }
        [Display(Name = "Danh mục")]
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Categories.Products))]
        public virtual Categories Category { get; set; }
    }
}
