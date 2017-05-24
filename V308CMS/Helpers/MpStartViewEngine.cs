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
        public MpStartViewEngine(Boolean setDefault = false)
        {
            var viewPath = "";
            if (setDefault)
            {
                var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
                string controllerName = routeValues["controller"].ToString();
                controllerName = controllerName.First().ToString().ToUpper() + String.Join("", controllerName.Skip(1));
                viewPath = string.Format("~/Views/{0}",controllerName);
            }
            else {
                viewPath = string.Format("~/Views/themes/{0}", ConfigHelper.Domain);
            }
            
            var viewRealPath =  HttpContext.Current.Server.MapPath(viewPath);
            var listDir = Directory.GetDirectories(viewRealPath, "*",SearchOption.AllDirectories);
            if (listDir.Length == 0)
            {
                //throw  new Exception("Can't find views directory to setup.");
                var locations = new string[1];
                //var viewDir = listDir[i].Replace(viewRealPath, "").Replace(@"\", "/");
                //locations[i] = viewPath + viewDir + "/{0}.cshtml";

                locations[0] = viewPath+"/{0}.cshtml";;
                this.ViewLocationFormats = locations;
                this.PartialViewLocationFormats = locations;
                this.MasterLocationFormats = locations;
                
            }
            else {
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
}