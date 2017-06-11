using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum QuantityFilterEnum
    {
        [Description("Chọn số lượng")]
        Disable = 0,
        [Description("Còn hàng")]
        Avaiable = 1,
        [Description("Hết hàng")]
        SoidOut = 2
    }
}