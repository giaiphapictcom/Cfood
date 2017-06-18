using System.ComponentModel.DataAnnotations;
using System.Web;

namespace V308CMS.Admin.Models
{
    public class ProductImageModels:BaseModels
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Thứ tự")]
        public string Number { get; set; }
        
        public HttpPostedFileBase Image { get; set; }
    }
}