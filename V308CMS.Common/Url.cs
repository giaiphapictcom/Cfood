using System;
using System.Web;

namespace V308CMS.Common
{
    public static class url
    {
        public static string article(string title="",int id = 0) {
            string anchor = "<a href=\"" + articleURL(title,id) + "\">" + title + "</a>";
            return anchor;
        }

        public static string articleURL(string title = "", int id = 0)
        {
            return "/" + Ultility.URITitle(title) + "-n" + id + ".html";
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

        public static HtmlString anchor_menu(string title = "", string src = "", string classname = "",string target="")
        {
            string domain = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;

            string target_url = "";
            string src_return = "";
            if (src != null && src.Length > 0)
            {
                if (Uri.IsWellFormedUriString(src, UriKind.Absolute))
                {
                    Uri myUri = new Uri(src);
                    string host = myUri.Host;

                    

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
                    src_return = src;
                }
                
            }
            else {
                src_return = "/";
            }

            target_url = "target=\"" + target + "\"";

            string anchor = "<a class=\""+classname+"\" href=\""+src_return+"\" title=\""+title+"\"  "+ target_url + " > <span>"+title+"</span></a>";
            return new HtmlString(anchor);
        }

        public static string Image(this string path, int width = 0, int height = 0)
        {
            string ImageUploadSource = System.Configuration.ConfigurationManager.AppSettings["ResourceDomain"] ?? String.Empty;
            if (ImageUploadSource.Length < 1)
            {
                return path;
            }
            else if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
            {
                return path;
            }


            string resizeDir = "";
            if (width > 0 && height > 0)
            {
                resizeDir = String.Format("w{0}h{1}", width, height);
            }
            else if (width > 0)
            {
                resizeDir = String.Format("w{0}", width);
            }
            else if (height > 0)
            {
                resizeDir = String.Format("h{0}", height);
            }
            path = path.Replace("\\", "/");
            var imgUploadPath = path.Replace("/Content/Images/", "").Trim();
            //imgUploadPath = imgUploadPath.Replace("\\Content\\Images\\", "");


            if (imgUploadPath.Length < 1)
            {
                imgUploadPath = "noimage.jpg";
            }
            return ImageUploadSource + "/" + resizeDir + "/" + imgUploadPath;
            //return ImageHelper.Crop(path, width,height);

        }
    }
}