using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

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
        [Required(ErrorMessage = " Nhập tên sản phẩm")]
        [StringLength(200)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [StringLength(200)]
        [Display(Name = "Mô tả sản phẩm")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Nhập đơn giá sản phẩm" )]
        [Display(Name = "Đơn giá")]
        [Column(TypeName = "decimal(18,0)")]
        public decimal? Price { get; set; }
        [StringLength(100)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Chọn danh mục")]
        [Display(Name = "Danh mục")]
        public int? CategoryId { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        [Column(TypeName = "decimal(18,0)")]
        public decimal? Discount { get; set; }
        [Display(Name = "Khuyến mãi")]
        public bool isDiscount { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Categories.Products))]
        public virtual Categories Category { get; set; }
        [InverseProperty("IdProductNavigation")]
        public virtual ICollection<CartDetails> CartDetails { get; set; }
    }
}
