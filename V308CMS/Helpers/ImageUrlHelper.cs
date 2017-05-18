using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Common;

namespace V308CMS.Helpers
{
    public static class ImageUrlHelper
    {
        public static string ToUrl(this string path)
        {
            return ImageHelper.Crop(path, 267,144);
        }
    }
}