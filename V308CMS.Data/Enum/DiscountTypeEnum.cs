using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum DiscountTypeEnum
    {
        [Description("Giảm theo đơn hàng")]
        ByItem = 1,
        [Description("Giảm theo sản phẩm")]
        BySubTotal = 2
    }
}