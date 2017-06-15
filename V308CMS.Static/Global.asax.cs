using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ImageResizer.Configuration;
using V308CMS.Static.Helpers;

namespace V308CMS.Static
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Config.Current.Pipeline.Rewrite += Pipeline_Rewrite;
        }

        private void Pipeline_Rewrite(IHttpModule sender, HttpContext context, IUrlEventArgs e)
        {
            if (e.VirtualPath == null)
            {
                context.RewritePath("index.html");
            }
            
                string fileCache = ImageHelper.checkFile(e.VirtualPath);
                context.RewritePath(fileCache);
            
            

            //string root = ConfigurationManager.AppSettings["ImageRoot"] ?? String.Empty;
            

            //root = "~/upload/";
            //var test = VirtualPathUtility.ToAbsolute(root);
            //if (e.VirtualPath.StartsWith(VirtualPathUtility.ToAbsolute(root), StringComparison.OrdinalIgnoreCase))
            //{
            //    e.VirtualPath = Regex.Replace(e.VirtualPath,
            //        string.Format("/{root}/img/([0-9]+)\\.([0-9]+)-([^/]+)\\.(jpg|jpeg|png|gif)"),
            //        delegate(Match match)
            //        {
            //            e.QueryString["width"] = match.Groups[1].Value;
            //            e.QueryString["height"] = match.Groups[2].Value;
            //            e.QueryString["mode"] = "scale";
            //            return string.Format("/{root}/img/{match.Groups[3].Value}.{match.Groups[4].Value}");
            //        });
            //    //e.VirtualPath = Regex.Replace(e.VirtualPath, string.Format("/{root}/img/([0-9]+)w-([^/]+)\\.(jpg|jpeg|png|gif)"),
            //    //    delegate(Match match)
            //    //    {
            //    //        e.QueryString["width"] = match.Groups[1].Value;
            //    //        e.QueryString["mode"] = "scale";
            //    //        return string.Format("/{root}/img/{match.Groups[2].Value}.{match.Groups[3].Value}");
            //    //    });
            //    //e.VirtualPath = Regex.Replace(e.VirtualPath, string.Format("/{root}/img/([0-9]+)h-([^/]+)\\.(jpg|jpeg|png|gif)"),
            //    //    delegate(Match match)
            //    //    {
            //    //        e.QueryString["height"] = match.Groups[1].Value;
            //    //        e.QueryString["mode"] = "scale";
            //    //        return string.Format("/{root}/img/{match.Groups[2].Value}.{match.Groups[3].Value}");
            //    //    });

            //    context.RewritePath(e.VirtualPath);

            //}
            //else {
            //    //using V308CMS.Static.Helpers;

            //    string fileCache = ImageHelper.checkFile(e.VirtualPath);
            //    context.RewritePath(fileCache);
            //    //var fInfo = new System.IO.FileInfo(e.VirtualPath);
            //    //var result = fInfo.DirectoryName;
                
            //}

        }
    }
}