using System.Collections.Generic;

namespace V308CMS.Admin.Models.UI
{
    public class MutilCategoryItem
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class MutilDropDownlistModels
    {
        public string Name { get; set; }
        public List<MutilCategoryItem> Data { get; set; }
        public int SelectedId { get; set; }
        public string PlaceHolder { get; set; }
        public string CssClass { get; set; }
        public int ParentId { get; set; }
        public string Selected { get; set; }
        public MutilDropDownlistModels()
        {
            CssClass = "form-control";
        }
    }

}