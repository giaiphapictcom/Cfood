using System;
using System.ComponentModel.DataAnnotations;
using V308CMS.Admin.DataAnnotations;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class EmailConfigModels:BaseModels
    {
        public EmailConfigModels()
        {
            State =(byte) StateEnum.Active;
            CreatedDate = DateTime.Now;

        }
  
        public int Id { get; set; }
        [Required]
        public byte Type { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên cấu hình Email.")]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập thông tin Host.")]
        [Display(Name = "Host")]
        public string Host { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập thông tin Port.")]
        [Display(Name = "Port")]
        public string Port { get; set; }
        [Required]
        [StringLength(255)]
        [CheckEmail(ErrorMessage = "Địa chỉ Email không đúng.")]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }
        [Required]
        [StringLength(255)]
       
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Trạng thái")]
        public byte State { get; set; }
       
    }
}