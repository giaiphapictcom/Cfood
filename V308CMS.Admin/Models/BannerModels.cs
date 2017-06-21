using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace V308CMS.Admin.Models
{
    public class BannerModels:BaseModels
    {
        public BannerModels()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Status = true;
        }
        public int Id { get; set; }
        public string Site { get; set; }

        [Display(Name = "Tên*")]
        [Required(ErrorMessage = "Vui lòng nhập tên Banner.")]
        [StringLength(50,ErrorMessage = "Tên banner không được vượt quá 50 ký tự.")]
        public string Name { get; set; }
        [Display(Name = "Mô tả")]
        [StringLength(50, ErrorMessage = "Nội dung mô tả không được vượt quá 250 ký tự.")]
        public string Description { get; set; }
        [Display(Name = "Vị trí")]
        public byte Position { get; set; }
        [Display(Name = "Chiều rộng")]
        public int Width { get; set; }
        [Display(Name = "Chiều cao")]
        public int Height { get; set; }
        [Display(Name = "Ngày bắt đầu hiển thị")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Ngày kết thúc hiển thị")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Đường dẫn ảnh")]
        [StringLength(2500, ErrorMessage = "Đường dẫn ảnh không được vượt quá 500 ký tự.")]
        public string ImageUrl { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}