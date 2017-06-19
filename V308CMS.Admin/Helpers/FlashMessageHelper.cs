using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

﻿using System.Web.Mvc;

namespace V308CMS.Admin.Helpers
{
    public static class FlashMessageHelper
    {
        public static string GetFlashMessage(this ViewContext  context,string key="Message")
        {
            return context.TempData[key] != null ? context.TempData[key].ToString() : "";

        }
    }
}