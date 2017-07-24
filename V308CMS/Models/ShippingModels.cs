using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Models
{
    public class ShippingModels
    {
        public ShippingModels()
        {
            Default = true;
            UpdatedAt = DateTime.Now;
        }
       
        public int UserId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Họ tên.")]
        [StringLength(250, ErrorMessage = "Họ tên không được vượt quá 250 ký tự.")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại.")]
        [Phone(ErrorMessage = "Số điện thoại không đúng.")]
        [Display(Name = "Điện thoại di động")]        
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Tỉnh/Thành phố.")]      
        [Display(Name = "Tỉnh/Thành phố")]
        public int Region { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Quận/Huyện.")]   
        [Display(Name = "Quận/Huyện")]
        public int City { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Phường/Xã.")]        
        [Display(Name = "Phường/Xã")]
        public int Ward { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Địa chỉ.")]
        [StringLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        public bool Default { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}