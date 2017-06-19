using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class ProductBrandModels:BaseModels
    {
        public ProductBrandModels()
        {
            Status = (byte)StateEnum.Active;
        }
        public int Id { get; set; }
        [Display(Name = "Loại sản phẩm")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Tên thương hiệu không được để trống.")]
        [StringLength(250,ErrorMessage = "Tên thương hiệu không được vượt quá 250 ký tự.")]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Ảnh")]
        public string ImageUrl { get; set; }
        [Display(Name = "Trạng thái")]
        public byte Status { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}