using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Sale.Helpers
{
    public class HtmlAffiliate
    {
        public static string MenuActive(string uri="",string classSelected = "selected"){
            string classname = "";
            string baseUrl = HttpContext.Current.Request.FilePath;
            if (baseUrl[0] == '/') {
                baseUrl = baseUrl.TrimStart('/');
            }
            if (uri == baseUrl.Split('/').FirstOrDefault()) {
                classname = classSelected;
            }
            else if (uri==baseUrl)
            {
                classname = classSelected;
            }
            return classname;
        }

        public static HtmlString li_anchor(string title, string uri = "") {
            
            string li_class = MenuActive(uri);
            string notify = "";
            if (uri.Length < 1) {
                li_class = "";
                notify = "<span class=\"notification-red\"></span>";
                uri = "javascript:void(0)";
            }
            string html = string.Format("<li class=\"{0}\"><a href=\"{1}\">{2} {3}</a></li>", li_class, uri, title, notify);
            return new HtmlString(html); ;

        }
    }
}