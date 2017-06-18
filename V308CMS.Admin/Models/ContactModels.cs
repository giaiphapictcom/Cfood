using System;
using System.ComponentModel.DataAnnotations;
using V308CMS.Admin.DataAnnotations;

namespace V308CMS.Admin.Models
{
    public class ContactModels:BaseModels
    {
        public ContactModels()
        {
            CreatedDate = DateTime.Now;
        }
        public  int Id { get; set; }
        [Display(Name="Họ tên")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        [CheckEmail(ErrorMessage = "Địa chỉ Email không đúng.")]
        [StringLength(250, ErrorMessage = "Địa chỉ Email không được vượt quá 250 ký tự.")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự.")]
        public string Phone { get; set; }
        [Display(Name = "Tin nhắn")]
        [StringLength(500, ErrorMessage = "Nội dung tin nhắn không được vượt quá 500 ký tự.")]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}