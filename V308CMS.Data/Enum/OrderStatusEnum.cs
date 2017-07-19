using System;
using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    [Flags]
    public enum OrderStatusEnum
    {
        [Description("Chờ duyệt")]
        Pending = 1,
        [Description("Đang sử lý")]
        Processing = 2,
        [Description("Đang giao hàng")]
        Delivering = 3,
        [Description("Hoàn tất")]
        Complete = 4,
        [Description("Hủy mua hàng")]
        CancelledOrder = 5,      
        [Description("Hủy thanh toán")]
        CanceelledPayment = 6,
        [Description("Trả lại hàng")]
        Refund = 7
    }

    public enum AccountStatusEnum
    {
        [Description("Chờ duyệt")]
        Processing = 0,

        [Description("Đang sử dụng")]
        Complete = 1,
        
        [Description("Khóa tài khoản")]
        Disable = -1
    }
}