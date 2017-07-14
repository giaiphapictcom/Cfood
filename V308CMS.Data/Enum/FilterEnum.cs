using System;
using System.ComponentModel;

namespace V308CMS.Data.Enum
{
   
    public enum FilterEnum
    {
        [Description("Không lọc")]
        NoFilter =0,
        [Description("Thương hiệu")]
        ByBrand = 1,
        [Description("Nhà sản xuất")]
        ByManufacturer =2,
        [Description("Từ Giá")]
        ByFromPrice = 3,
        [Description("Tới Giá")]
        ByToPrice = 4,
        [Description("Trong khoảng giá")]
        ByBetweenPrice = 5
    }
}