using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using V308CMS.Data;

namespace V308CMS.Admin.Models
{
    public  class OrderModels:BaseModels
    {
        [Display(Name = "Order #")]
        public int Id { get; set; }
        [Display(Name = "Họ tên khách hàng")]
        [StringLength(100, ErrorMessage = "Họ tên khách hàng không được vượt quá 100 ký tự.")]
        public string FullName { get; set; }
        [Display(Name = "Số điện thoại")]
        [StringLength(100, ErrorMessage = "Số điện thoại không được vượt quá 100 ký tự.")]
        public string Phone { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime Date { get; set; }
        [Display(Name = "Địa chỉ")]
        [StringLength(100, ErrorMessage = "Số điện thoại không được vượt quá 250 ký tự.")]
        public string Address { get; set; }
        [Display(Name = "Tổng tiền")]
        public int TotalPrice { get; set; }
        [Display(Name = "Ghi chú")]
        public string Detail { get; set; }
        [Display(Name = "Trạng thái")]
        public byte Status { get; set; }
        public List<productorder_detail> OrderDetail { get; set; }

    }
}