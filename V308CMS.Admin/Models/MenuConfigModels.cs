using System;
using System.ComponentModel.DataAnnotations;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class MenuConfigModels:BaseModels
    {

        public MenuConfigModels()
        {
            State = (int) StateEnum.Active;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Tên menu.")]
        [StringLength(50, ErrorMessage = "Tên Menu không được vượt quá 50 ký tự.")]      
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [StringLength(255,ErrorMessage = "Nội dung mô tả không được vượt quá 255 ký tự.")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [StringLength(50, ErrorMessage = "Mã không được vượt quá 50 ký tự.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập liên kết.")]
        [StringLength(500,ErrorMessage = "Liên kết không được vượt quá 500 ký tự.")]
       
        public string Link { get; set; }
        [Display(Name = "Trạng thái")]
        public byte State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Order { get;set;}
        public byte Site { get; set; }
        [StringLength(50, ErrorMessage = "Targe không được vượt quá 50 ký tự.")]
        public string Target { get; set; }
    }
}