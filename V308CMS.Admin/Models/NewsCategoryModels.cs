using System;
using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.Models
{
    public class NewsCategoryModels:BaseModels
    {
        public NewsCategoryModels()
        {           
            CreatedDate=DateTime.Now;
            State = true;
        }
       
        public int Id { get; set; }
        public string Site { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Tên chuyên mục.")]
        [Display(Name = "Tên chuyên mục")]
        public string Name { get; set; }
        [Display(Name = "Trạng thái")]
        public bool State { get; set; }
        [Display(Name = "Thứ tự")]
        public int Number { get; set; }
        [Display(Name = "Danh mục cha")]
        public int ParentId { get;set; }
        public DateTime CreatedDate { get; set; }
    
    }
}