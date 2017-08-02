using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum SortEnum:int
    {
      [Description("Ngày đăng")]
       Default =0,
       [Description("Giá tăng dần")]
       PriceAsc = 1,
       [Description("Giá giảm dần")]
       PriceDesc = 2,
       [Description("Tên từ A-Z")]
       NameAsc = 3,
       [Description("Tên từ Z-A")]
       NameDesc = 4,
       [Description("Sản phẩm mới")]
       DateDesc = 5,
       [Description("Sản phẩm cũ")]
       DateAsc = 6,
       [Description("Bán chạy nhất")]
       BestSelling = 7

    }
}