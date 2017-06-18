using System;
using System.ComponentModel.DataAnnotations;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class ProductColorModels:BaseModels
    {
        public ProductColorModels()
        {
            CreatedAt  = DateTime.Now;
            UpdatedAt = DateTime.Now;
            State = (byte) StateEnum.Active;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên màu không được để trống.")]
        [StringLength(50,ErrorMessage = "Tên màu không được vượt quá  50 ký tự.")]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mã màu không được để trống.")]
        [StringLength(50, ErrorMessage = "Mã màu không được vượt quá  50 ký tự.")]
        [Display(Name = "Mã")]
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Display(Name = "Trạng thái")]
        public byte State { get; set; }
        [StringLength(255, ErrorMessage = "Nội dung mô tả không được vượt quá 255 ký tự.")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
    }
}