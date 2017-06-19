using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.Models
{
    public class CountryModels:BaseModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên Quốc gia.")]
        [StringLength(50, ErrorMessage = "Tên quốc gia không được vượt quá 50 ký tự.")]
        [Display(Name = "Tên")]
        public string Name { get; set; }
    }
}