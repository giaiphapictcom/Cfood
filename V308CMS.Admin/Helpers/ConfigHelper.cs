using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Common;

namespace V308CMS.Admin.Helpers
{
    public class ConfigHelper
    {
        public static string ImageDomain
        {
            get { return Configs.GetItemConfig("ImageDomain"); }
        }
    }
}