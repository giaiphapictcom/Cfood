using System;
using System.Web;
using System.ComponentModel.DataAnnotations;

using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class VoucherModels : BaseModels
    {
        public VoucherModels()
        {
            Created = DateTime.Now;
            UpdateDate = DateTime.Now;
            Status = true;
            Site = Data.Helpers.Site.home;
        }
        public int Id { get; set; }
        public string Site { get; set; }

        [Display(Name = "Mã Sản Phẩm")]
        public string ProductCode { get; set; }

        [Display(Name = "Lượng giảm giá")]
        public float discount { get; set; }

        [Display(Name = "Mã giảm giá")]
        [Required(ErrorMessage = "Mã Voucher không được để trống.")]
        public string Code { get; set; }

        public string Type { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase ImgNew { get; set; }
        
        [Display(Name = "Tên*")]
        [Required(ErrorMessage = "Tên Voucher không được để trống.")]
        [StringLength(50,ErrorMessage = "Tên Voucher không được vượt quá 50 ký tự.")]
        public string Title { get; set; }

        [StringLength(255, ErrorMessage = "Nội dung môt tả không được vượt quá 255 ký tự.")]
        public string Summary { get; set; }

        [Display(Name = "Nội dung chi tiết")]
        public string Content { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Status { get; set; }
    }
}