using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Helpers
{
    public class MpStartViewEngine: RazorViewEngine
    {
        public MpStartViewEngine()
        {
            var viewPath = string.Format("~/Views/themes/{0}", ConfigHelper.Domain);
            var viewRealPath =  HttpContext.Current.Server.MapPath(viewPath);
            var listDir = Directory.GetDirectories(viewRealPath, "*",SearchOption.AllDirectories);
            if (listDir.Length == 0){
               throw  new Exception("Can't find views directory to setup.");
            }
            var locations = new string[listDir.Length];
            for (int i = 0; i < listDir.Length; i++)
            {
                var viewDir = listDir[i].Replace(viewRealPath, "").Replace(@"\", "/");
                locations[i] = viewPath + viewDir + "/{0}.cshtml";
            }
            this.ViewLocationFormats = locations;
            this.PartialViewLocationFormats = locations;
            this.MasterLocationFormats = locations;
        }

    }
}