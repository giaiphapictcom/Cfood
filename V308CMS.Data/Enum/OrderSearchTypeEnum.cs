using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum OrderSearchTypeEnum
    {
        [Description("Tất cả")]
        All =0,
        [Description("Theo họ tên")]
        ByName = 1,
        [Description("Theo số điện thoại")]
        ByPhone = 2,
        [Description("Theo địa chỉ")]
        ByAddress =3
    }
}