using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace V308CMS.Admin.Models
{
    public class ProductTypeModels:BaseModels
    {
        public ProductTypeModels()
        {
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public int Id { get; set; }
        public string Icon { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Thứ tự")]
        public int Number { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Tên loại sản phẩm không được để trống.")]
        [StringLength(250,  ErrorMessage = "Tên loại sản phẩm không được vượt quá 250 ký tự")]
        public string Name { get; set; }
        [Display(Name = "Chuyên mục cha")]
        public int ParentId { get; set; }
        [Display(Name = "Ghi chú")]
        public string Description { get; set; }
        [Display(Name = "Mô tả")]
        public string Detail { get; set; }
       
        
        public string ImageUrl { get; set; }
        public string ImageBannerUrl { get; set; }
        [Display(Name = "Ảnh banner trang chủ")]
        public HttpPostedFileBase Image { get; set; }
        [Display(Name = "Ảnh banner")]
        public HttpPostedFileBase ImageBanner { get; set; }

        [Display(Name = "Hiển thị trang chủ")]
        public bool IsHome { get; set; }
       
    }
}