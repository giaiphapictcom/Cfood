using System.Collections.Generic;

namespace V308CMS.Common.UI
{
    public class OptionItem
    {
        public int Id { get; set; }
        public string value { get; set; }
        public string Name { get; set; }
    }
    public class OptionsModel
    {
        public string Name { get; set; }
        public List<OptionItem> Data { get; set; }
        public string Selected { get; set; }
        public string PlaceHolder { get; set; }
        public string CssClass { get; set; }
        public int ParentId { get; set; }
        public OptionsModel()
        {
            CssClass = "form-control";
        }
    }
}