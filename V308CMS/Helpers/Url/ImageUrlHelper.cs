using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Common;

namespace V308CMS.Helpers
{
    public static class ImageUrlHelper
    {
        public static string ToUrl(this string path, int width =848, int height =458)
        {
            return ImageHelper.Crop(path, width,height);
            
        }
    }
}