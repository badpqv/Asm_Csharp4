using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
        [StringLength(10)]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
