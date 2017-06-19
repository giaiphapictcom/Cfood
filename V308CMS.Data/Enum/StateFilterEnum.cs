using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum StateFilterEnum
    {
        [Description("Chọn trạng thái")]
        All = 0,
        [Description("Đã duyệt")]
        Active = 1,
        [Description("Chờ duyệt")]
        Pending = 2,
        [Description("Chưa nhập giá")]
        PriceEmpty =3
            

    }
}