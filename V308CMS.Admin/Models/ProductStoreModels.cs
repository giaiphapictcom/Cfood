using System;
using System.ComponentModel.DataAnnotations;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class ProductStoreModels:BaseModels
    {
        public ProductStoreModels()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            State = (byte) StateEnum.Active;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên kho hàng không được để trống.")]
        [StringLength(250,ErrorMessage = "Tên kho hàng không được vượt quá 250 ký tự.")]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [StringLength(250, ErrorMessage = "Tên kho hàng không được vượt quá 15 ký tự.")]
        [Phone(ErrorMessage = "Số điện thoại không đúng.")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [StringLength(50, ErrorMessage = "Họ tên người quản lý không được vượt quá 50 ký tự.")]
        [Display(Name = "Người quản lý")]
        public string Manager { get; set; }
        [StringLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Display(Name = "Trạng thái")]
        public byte State { get; set; }
    }
}