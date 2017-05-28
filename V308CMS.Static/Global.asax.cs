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
            var root = ConfigurationManager.AppSettings["ImageRoot"] ?? String.Empty;


            if (e.VirtualPath.StartsWith(VirtualPathUtility.ToAbsolute($"~/{root}/"), StringComparison.OrdinalIgnoreCase))
            {
                e.VirtualPath = Regex.Replace(e.VirtualPath,
                    $@"/{root}/img/([0-9]+)\.([0-9]+)-([^/]+)\.(jpg|jpeg|png|gif)",
                    delegate(Match match)
                    {
                        e.QueryString["width"] = match.Groups[1].Value;
                        e.QueryString["height"] = match.Groups[2].Value;
                        e.QueryString["mode"] = "scale";
                        return $"/{root}/img/{match.Groups[3].Value}.{match.Groups[4].Value}";
                    });
                e.VirtualPath = Regex.Replace(e.VirtualPath, $@"/{root}/img/([0-9]+)w-([^/]+)\.(jpg|jpeg|png|gif)",
                    delegate(Match match)
                    {
                        e.QueryString["width"] = match.Groups[1].Value;
                        e.QueryString["mode"] = "scale";
                        return $"/{root}/img/{match.Groups[2].Value}.{match.Groups[3].Value}";
                    });
                e.VirtualPath = Regex.Replace(e.VirtualPath, $@"/{root}/img/([0-9]+)h-([^/]+)\.(jpg|jpeg|png|gif)",
                    delegate(Match match)
                    {
                        e.QueryString["height"] = match.Groups[1].Value;
                        e.QueryString["mode"] = "scale";
                        return $"/{root}/img/{match.Groups[2].Value}.{match.Groups[3].Value}";
                    });

                context.RewritePath(e.VirtualPath);

            }

        }
    }
}