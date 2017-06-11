using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum AccountType
    {
        [Description("Nhà cung cấp")]
        Provider =1,
        [Description("Hệ thống")]
        System = 2
    }
}