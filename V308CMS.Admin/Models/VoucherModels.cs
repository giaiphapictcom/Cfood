using System;
using System.ComponentModel.DataAnnotations;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class VoucherModels
    {
        public VoucherModels()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            State =(byte) StateEnum.Active;
        }
        public int Id { get; set; }
        [Display(Name = "Tên*")]
        [Required(ErrorMessage = "Tên Voucher không được để trống.")]
        [StringLength(50,ErrorMessage = "Tên Voucher không được vượt quá 50 ký tự.")]
        public string Name { get; set; }
        [StringLength(255, ErrorMessage = "Nội dung môt tả không được vượt quá 255 ký tự.")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ExpireDate { get; set; }
        public byte State { get; set; }
    }
}