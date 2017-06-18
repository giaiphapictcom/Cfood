using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Helpers.UI
{
    public static class NewsCategoryUiHelper
    {    
        public static HtmlString MutilDropdownList(this HtmlHelper helper,
             string name = "",
             List<NewsGroups> data = null,
             int selected = 0,
             string placeHolder = "",
             string cssClass= "form-control")
        {
            var sbHtmlBuilder = new StringBuilder();
            sbHtmlBuilder.Append($"<select class='{cssClass}' name='{name}' id='{name}' placeholder={placeHolder}>");
            sbHtmlBuilder.Append(
                selected == 0
                ? "<option value='0' selected>Tất cả các loại</option>"
                : "<option value='0'>Tất cả các loại</option>");
            sbHtmlBuilder.Append(InternalBuildDropdownListMutil(data, selected));
            sbHtmlBuilder.Append("</select>");
            return new HtmlString(sbHtmlBuilder.ToString());
        }

    

        private static string InternalBuildDropdownListMutil(
             
            List<NewsGroups> data = null,
            int selected = 0,
            int parentId =0,
            string strLimit = ""
            )
        {
            var sbHtmlBuilder = new StringBuilder();
            if (data != null && data.Count > 0)
            {
              
                foreach (var item in data)
                {
                    if (item.Parent == parentId)
                    {
                       
                        if (selected != 0 && item.ID == selected)
                        {
                            sbHtmlBuilder.Append($"<option value='{item.ID}' selected>{strLimit} {item.Name}</option>");
                        }
                        else
                        {
                            sbHtmlBuilder.Append($"<option value='{item.ID}'>{strLimit} {item.Name}</option>");
                        }
                        sbHtmlBuilder.Append(InternalBuildDropdownListMutil(data, selected, item.ID, strLimit + "---|"));
                      
                    }

                }
            }
            return sbHtmlBuilder.ToString();
        }
        
    }
}