using System;
using System.ComponentModel.DataAnnotations;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class ProductUnitModels:BaseModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên đơn vị tính không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên đơn vị tính không được vượt quá 50 ký tự.")]
        [Display(Name ="Tên")]
        public string Name { get; set; }
        public byte State { get; set; }
        [Display(Name = "Trạng thái")]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ProductUnitModels()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            State = (byte) StateEnum.Active;
        }
    }
}