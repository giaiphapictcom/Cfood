using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Admin.Models
{
    public class ProductModels:BaseModels
    {
        public ProductModels()
        {
            ListProductImages = new List<ProductImage>();            
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Tên sản phẩm.")]
        [StringLength(250, ErrorMessage = "Tên sản phẩm không được vượt quá 250 ký tự.")]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Nội dung mô tả ngắn về sản phẩm.")]
        [StringLength(1000, ErrorMessage = "Mô tả ngắn không được vượt quá 1000 ký tự.")]
        [Display(Name="Mô tả ngắn")]
        public string Summary { get; set; }
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }
        [StringLength(50,ErrorMessage = "Mã sản phẩm không được vượt quá 50 ký tự.")]
        [Display(Name = "Mã")]
        public string Code { get; set; }
        [StringLength(250,ErrorMessage = "Ảnh đại diện không được vượt quá 250 ký tự.")]
        public string ImageUrl { get; set; }
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Thương hiệu")]
        public int? BrandId { get; set; }

        [Display(Name = "Nước sản xuất")]
        public string Country { get; set; }

        [Display(Name = "Kho hàng")]
        public string Store { get; set; }
        [Display(Name = "Nhà sản xuất")]
        public int? ManufacturerId { get; set; }       
        [Display(Name = "Nhà cung cấp")]
        public string ProviderId { get; set; }
        [Display(Name = "Thứ tự")]
        public int Number { get; set; }
        [Display(Name = "Đơn vị tính")]
        public string Unit { get; set; }
        [Display(Name = "Trọng lượng")]
        public string Weight { get; set; }
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        [Display(Name = "Màu sắc")]
        public string[] Color { get; set; }
       
        [Display(Name = "Mức Chiết khấu NPP(%)")]
        public string Npp { get; set; }
        [Display(Name = "Kích cỡ")]
        public string[] Size { get; set; }
        [Display(Name = "Chiều rộng")]
        public string Width { get; set; }
        [Display(Name = "Chiều cao")]
        public string Height { get; set; }
        [Display(Name = "Độ dày")]
        public string Depth { get; set; }

        [Display(Name = "Chi tiết sản phẩm")]        
        public string Detail { get; set; }

        [Display(Name = "Thời gian bảo hành")]
        public string WarrantyTime { get; set; }
        [Display(Name = "Hạn sử dụng")]
        public string ExpireDate { get; set; }
        [Display(Name = "Tiêu đề")]
        [StringLength(250, ErrorMessage = "Tiêu đề không được vượt quá 250 ký tự.")]
        public string MetaTitle { get; set; }

        [Display(Name = "Từ khóa")]
        [StringLength(500,ErrorMessage = "Từ khóa không được vượt quá 500 ký tự.")]
        public string MetaKeyword { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(500, ErrorMessage = "Nội dung mô tả không được vượt quá 500 ký tự.")]
        public string MetaDescription { get; set; }
        [Display(Name = "Giá")]
        public int Price { get; set; }
        [Display(Name = "Giảm giá")]
        public string Percent { get; set; }
        [Display(Name = "Ngày bắt đầu")]
        public string StartDate { get; set; }
        [Display(Name = "Ngày kết thúc")]
        public string EndDate { get; set; }
        public string[] AttrLabel { get; set; }
        public string[] AttrDesc { get; set; }
        public string[] ProductImages { get; set; }
        [Display(Name = "Từ")]
        public int Transport1 { get; set; }
        [Display(Name = "Đến")]
        public int Transport2 { get; set; }

        public List<ProductImage> ListProductImages { get; set; }
        public List<ProductColor> ListProductColor { get; set; }
        public List<ProductAttribute> ListProductAttribute { get; set; }
        public List<ProductSize> ListProductSize { get; set; }
        public List<ProductSaleOff> ListProductSaleOff { get; set; }

        public int? AccountId { get; set; }


    }
}