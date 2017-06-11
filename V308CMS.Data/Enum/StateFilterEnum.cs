using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Enum
{
    public enum StateFilterEnum
    {
        [Description("Chọn trạng thái")]
        Disable = 0,
        [Description("Đã duyệt")]
        Active = 1,
        [Description("Chờ duyệt")]
        Pending = 2

    }
}