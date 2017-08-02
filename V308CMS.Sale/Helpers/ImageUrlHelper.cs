using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Common;

namespace V308CMS.Sale.Helpers
{
    public static class ImageUrlHelper
    {
        public static string ToUrl(this string path, int width =0, int height =0)
        {
            string ImageUploadSource = System.Configuration.ConfigurationManager.AppSettings["ResourceDomain"] ?? String.Empty;
            if (ImageUploadSource.Length < 1) {
                return path;
            }
            else if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
            {
                return path;
            }

            
            string resizeDir = "";
            if (width > 0 && height > 0)
            {
                resizeDir = String.Format("w{0}h{1}", width, height);
            }
            else if (width > 0)
            {
                resizeDir = String.Format("w{0}", width);
            }
            else if (height > 0)
            {
                resizeDir = String.Format("h{0}", height);
            }
            path = path.Replace("\\","/");
            var imgUploadPath = path.Replace("/Content/Images/", "").Trim();
            //imgUploadPath = imgUploadPath.Replace("\\Content\\Images\\", "");


            if (imgUploadPath.Length < 1) {
                imgUploadPath = "noimage.jpg";
            }
            return ImageUploadSource + "/" + resizeDir + "/" + imgUploadPath;
            //return ImageHelper.Crop(path, width,height);
            
        }
    }
}