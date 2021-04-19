using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Asm_Csharp4.Models
{
    public partial class Customers
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Họ tên người dùng")]
        [StringLength(50)]
        public string FullName { get; set; }
        [Display(Name = "Tên tài khoản")]
        [StringLength(100)]
        public string TaiKhoan { get; set; }
        [Display(Name = "Mật khẩu")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
        [Display(Name = "Quyền")]
        [StringLength(20)]
        public string Quyen { get; set; }
    }
}
