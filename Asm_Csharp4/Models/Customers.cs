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
        public Customers()
        {
            CartDetails = new HashSet<CartDetails>();
            Carts = new HashSet<Carts>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Display(Name = "Họ tên người dùng")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "CMND / CCCD")]
        [Column("CmndCCCD")]
        [StringLength(12)]
        public string CmndCccd { get; set; }
        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }
        [StringLength(50)]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(30)]
        [Display(Name = "Quyền")]
        public string Quyen { get; set; }
        [Column("SDT")]
        [StringLength(11)]
        public string SDT { get; set; }

        [InverseProperty("IdCustomerNavigation")]
        public virtual ICollection<CartDetails> CartDetails { get; set; }
        [InverseProperty("IdCustomerNavigation")]
        public virtual ICollection<Carts> Carts { get; set; }
    }
}
