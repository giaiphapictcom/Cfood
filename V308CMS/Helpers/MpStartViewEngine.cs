using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Helpers
{
    public class MpStartViewEngine: RazorViewEngine
    {
        public MpStartViewEngine()
        {
            string[] locations = {
                "~/Views/themes/myshopify/Blocks/{0}.cshtml",
                "~/Views/themes/myshopify/Pages/{0}.cshtml",
                "~/Views/themes/myshopify/Menu/{0}.cshtml",
                "~/Views/themes/myshopify/Layout/{0}.cshtml",
                "~/Views/themes/myshopify/Banner/{0}.cshtml",
                "~/Views/themes/myshopify/Product/{0}.cshtml",
                "~/Views/themes/myshopify/Widget/{0}.cshtml",
                "~/Views/themes/myshopify/Blocks/Filter/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/PartialViews/{0}.cshtml",
                "~/Views/Shared/Layouts/{0}.cshtml"

            };

            this.ViewLocationFormats = locations;
            this.PartialViewLocationFormats = locations;
            this.MasterLocationFormats = locations;           

        }

    }
}