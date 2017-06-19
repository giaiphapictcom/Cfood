using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum EmailType
    {
        [Description("Gmail")]
        Gmail =1,
        [Description("Host Mail")]
        HostMail = 2,
        [Description("Yahoo Mail")]
        YahooMail = 3,
        [Description("Khác")]
        Other = 4,
    }
}