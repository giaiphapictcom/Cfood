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
    }
}