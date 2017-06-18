using System;
using System.ComponentModel.DataAnnotations;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class PermissionModels:BaseModels
    {
        public PermissionModels()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Status = (byte)StateEnum.Active;
        }
        public int Id { get; set; }
        [Display(Name = "Nhóm quyền*")]
        public string GroupCode { get; set; }
        [Display(Name = "Hành động*")]
        public string Action { get; set; }               
        [Display(Name = "Trạng thái")]
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}