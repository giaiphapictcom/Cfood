using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace V308CMS.Admin.Models
{
    public class NewsModels : BaseModels
    {
        public NewsModels()
        {
            Status = true;
            CreatedAt = DateTime.Now;          
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
        [StringLength(255,ErrorMessage = "Tiêu đề tin không được vượt quá 255 ký tự.")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung tóm tắt.")]
        [StringLength(255, ErrorMessage = "Nội dung tóm tắt không được vượt quá 255 ký tự.")]
        [Display(Name = "Tóm tắt")]
        public string Summary { get; set; }
        [Display(Name = "Chuyên mục")]
        public int CategoryId { get; set; }
        [Display(Name = "Ảnh đại diện")]     
        public string ImageUrl { get; set; }

        public HttpPostedFileBase Image { get; set; }
        [Display(Name="Nội dung")]
        public string Detail { get; set; }
        [Display(Name = "Từ khóa(SEO)")]
        [StringLength(255, ErrorMessage = "Danh sách từ khóa không được vượt quá 255 ký tự")]
        public string Keyword { get; set; }
        [Display(Name = "Mô tả(SEO)")]
        [StringLength(255, ErrorMessage = "Nội dung mô tả không được vượt quá 255 ký tự")]
        public string Description { get; set; }
        [Display(Name = "Thứ tự")]
        public int Order { get; set; }

        [Display(Name="Hot")]
        public bool IsHot { get; set; }
        [Display(Name = "Phổ biến")]

        public bool IsFast { get; set; }

        [Display(Name = "Đề xuất")]

        public bool IsFeatured { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Site")]
        public string Site { get; set; } 

    }
}