using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace V308CMS.Data
{
    public class Theme
    {
        public  static string domain = ConfigurationManager.AppSettings["domain"];
        static public string viewPage(string file=null){
          
            string themePath = "~/Views/themes/" + domain;
            string themeRealPath = HttpContext.Current.Server.MapPath(themePath);

            string view = "";
            if (domain.Length > 0 && Directory.Exists(themeRealPath) )
            {
                if (System.IO.File.Exists(themeRealPath + "/pages/" + file + ".cshtml"))
                {
                    view = themePath + "/pages/" + file + ".cshtml";
                }
            }
            return view;
        }
    }
}