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
        [StringLength(50)]
        [Display(Name = "Họ tên khách hàng")]
        public string FullName { get; set; }
        [StringLength(100)]
        [Display(Name = "Tên tài khoản")]
        public string TaiKhoan { get; set; }
        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
        [StringLength(20)]
        [Display(Name = "Quyền")]
        public string Quyen { get; set; }
    }
}
