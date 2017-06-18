using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum DiscountTypeEnum
    {
        [Description("Giảm theo đơn hàng")]
        MpStart = 1,
        [Description("Giảm theo sản phẩm")]
        Affiliate = 2
    }
}