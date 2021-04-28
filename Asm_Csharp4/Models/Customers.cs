using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required(ErrorMessage = "Nhập tên người dùng")]
        [StringLength(50)]
        [Display(Name = "Họ tên người dùng")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Nhập địa chỉ email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "CMND / CCCD")]
        [Column("CmndCCCD")]
        [StringLength(12)]
        public string CmndCccd { get; set; }
        [Required(ErrorMessage = "Nhập địa chỉ người dùng")]
        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [StringLength(50)]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Nhập quyền")]
        [StringLength(30)]
        [Display(Name = "Quyền")]
        public string Quyen { get; set; }
        [Required(ErrorMessage = "Nhập SĐT")]
        [Column("SDT")]
        [StringLength(11)]
        public string SDT { get; set; }

        [InverseProperty("IdCustomerNavigation")]
        public virtual ICollection<CartDetails> CartDetails { get; set; }
        [InverseProperty("IdCustomerNavigation")]
        public virtual ICollection<Carts> Carts { get; set; }
    }
}
