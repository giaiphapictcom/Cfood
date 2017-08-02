using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum TransactionEnum:byte
    {
        [Description("Bắt đầu")]
        Start = 0,
        [Description("Hoàn tất")]
        Finish = 1
    }
}