using System.ComponentModel;

namespace V308CMS.Data.Enum
{
    public enum ProductCategoryLevelEnum
    {
        [Description("Danh mục gốc")]
        Root =0,
        [Description("Danh mục cha")]
        Parent = 1,
        [Description("Danh mục con")]
        Child = 2
    }
}