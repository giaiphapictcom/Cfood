using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Common
{
    public class url
    {
        public static string article(string title="",int id = 0) {
            string url = "/" + Ultility.URITitle(title) + "-n" + id + ".html";
            string anchor = "<a href=\"" + url + "\">" + title + "</a>";
            return anchor;
        }

        public static HtmlString productCategory(string title = "", int id = 0)
        {
            string url = "/" + Ultility.URITitle(title) + "-t" + id + ".html";
            string anchor = "<a href=\"" + url + "\">" + title + "</a>";
            return new HtmlString(anchor);
        }

        public static string productCategoryURL(string title = "", int id = 0)
        {
            string url = "/" + Ultility.URITitle(title) + "-t" + id + ".html";
            //string anchor = "<a href=\"" + url + "\">" + title + "</a>";
            return url;
        }

        public static string videoURL(string title = "", int id = 0)
        {
            return "/" + Ultility.URITitle(title) + "-youtube" + id + ".html";
        }

        public static string imgArticle(string img="",string title = "", int id = 0)
        {
            string anchor = string.Empty;
            return anchor;
        }
    }
}