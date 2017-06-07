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
            string url = productCategoryURL(title,id);
            string anchor = "<a href=\"" + url + "\">" + title + "</a>";
            return new HtmlString(anchor);
        }

        public static string productCategoryURL(string title = "", int id = 0)
        {
            string url = "/" + Ultility.URITitle(title) + "-t" + id + ".html";
            return url;
        }

        public static string productURL(string title = "", int id = 0, string ext = "html")
        {
            string url = "/" + Ultility.URITitle(title) + "-d" + id + "." + ext;
            return url;
        }

        public static HtmlString VideoAnchor(string title = "", int id = 0)
        {
            string url = videoURL(title, id);
            string anchor = "<a href=\"" + url + "\">" + title + "</a>";
            return new HtmlString(anchor);
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

        public static string WishlistIndexUrl()
        {
            string anchor = string.Empty;
            return anchor;
        }

        public static HtmlString anchor_menu(string title = "", string src = "", string classname = "")
        {
            string domain = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;

            string target = "";
            string src_return = "";
            if (src.Length > 0)
            {
                Uri myUri = new Uri(src);
                string host = myUri.Host;
                if (host != domain)
                {
                    target = "target=\"_blank\"";
                }

                if (host.Length < 1)
                {
                    src_return = "//" + host;
                    if (port != 80)
                    {
                        src_return += ":" + port.ToString();
                    }
                    src_return += "/" + src;
                }
                else
                {
                    src_return = src;
                }
            }
            else {
                src_return = "/";
            }
            
            
            string anchor = "<a class=\""+classname+"\" href=\""+src_return+"\" title=\""+title+"\"  "+target+" > <span>"+title+"</span></a>";
            return new HtmlString(anchor);
        }
        
        
    }
}