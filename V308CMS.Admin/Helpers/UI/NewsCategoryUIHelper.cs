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
            sbHtmlBuilder.Append( string.Format("<select class='{0}' name='{1}' id='{1}' placeholder={2}>",cssClass,name,placeHolder) );
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
                            sbHtmlBuilder.Append( string.Format("<option value='{0}' selected>{1} {2}</option>",item.ID,strLimit,item.Name) );
                        }
                        else
                        {
                            sbHtmlBuilder.Append(string.Format("<option value='{0}'>{1} {2}</option>", item.ID, strLimit, item.Name));
                        }
                        sbHtmlBuilder.Append(InternalBuildDropdownListMutil(data, selected, item.ID, strLimit + "---|"));
                      
                    }

                }
            }
            return sbHtmlBuilder.ToString();
        }
        
    }
}