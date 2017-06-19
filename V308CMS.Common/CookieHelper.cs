using System;
using System.Web;

namespace V308CMS.Common
{
    public class CookieHelper
    {
        private static CookieHelper _instance;

        private CookieHelper()
        {

        }

        public static CookieHelper Instance
        {
            get { return _instance ?? (_instance = new CookieHelper()); }
        }

        public void Add(string name, string value)
        {
            var cookie = new HttpCookie(name, value)
            {
                Expires = DateTime.Now.AddYears(1)
            };
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

        public void Save(string name, string value)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

        }

        public HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        public void Remove(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-1);
            }

        }

    }
}