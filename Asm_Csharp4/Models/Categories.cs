using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm_Csharp4.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nhập tên danh mục")]
        [StringLength(30)]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
