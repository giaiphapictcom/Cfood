using System;
using System.Web;

namespace V308CMS.Common
{
    public class CookieClass
    {
        public string GetCookie(string CookieName, string ValueIfNull, int ExpDate)
        {
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                return HttpContext.Current.Request.Cookies[CookieName].Value;
            }
            else
            {
                SetCookie(CookieName, ValueIfNull, ExpDate);
                return ValueIfNull;
            }
        }
        public void SetCookie(string CookieName, string CookieValue,int ExpDate)
        {
            HttpCookie aCookie = new  HttpCookie(CookieName);
            //aCookie.Domain = "thienma.vn";
            aCookie.Value = CookieValue;
            aCookie.Expires = DateTime.Now.AddDays(ExpDate);
            HttpContext.Current.Response.Cookies.Add(aCookie);
        }
        public void SetCookieWithoutExp(string CookieName, string CookieValue)
        {
            HttpCookie aCookie = new HttpCookie(CookieName);
            aCookie.Value = CookieValue;
            HttpContext.Current.Response.Cookies.Add(aCookie);
        }
    }

    public class CookieService
    {
        static CookieClass myCookies = new CookieClass();
        public static string GetCookie(string CookieName, string ValueIfNull, int ExpDate)
        {
            return myCookies.GetCookie(CookieName,ValueIfNull,ExpDate);
        }
        public static void SetCookie(string CookieName, string CookieValue, int ExpDate)
        {
            myCookies.GetCookie(CookieName,CookieValue,ExpDate);
        }
        public static string GetUrl()
        {
            if (HttpContext.Current.Request.Cookies["UrlBack"] != null)
            {
                return HttpContext.Current.Request.Cookies["UrlBack"].Value;
            }
            else
            {
                return AppSetting.URL;
            }
        }
        public static string Get(string pName)
        {
            if (HttpContext.Current.Request.Cookies[pName] != null)
            {
                return HttpContext.Current.Request.Cookies[pName].Value;
            }
            else
            {
                return null;
            }
        }
        public static void SetCookie2(string CookieName, string CookieValue, int ExpDate)
        {
             //neu co roi thi chang gia tri,neu chua co thi tao moi
            //nhu nhau het
            myCookies.SetCookie(CookieName, CookieValue, ExpDate);

        }
        public static void DeleteCookie(string CookieName)
        {
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                HttpCookie aCookie = new HttpCookie(CookieName);
                //aCookie.Domain = "thienma.vn";
                aCookie.Value = "0";
                aCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
        }
        public static string getUrlNapTien()
        {
            if (HttpContext.Current.Request.Cookies["Domain"] != null)
            {
                return HttpContext.Current.Request.Cookies["Domain"].Value;
            }
            else
            {
                return AppSetting.URL;
            }
        }
        public static void SetCookie3(string CookieName, string CookieValue)
        {
            //neu co roi thi chang gia tri,neu chua co thi tao moi
            //nhu nhau het
            myCookies.SetCookieWithoutExp(CookieName, CookieValue);

        }
    }
}
