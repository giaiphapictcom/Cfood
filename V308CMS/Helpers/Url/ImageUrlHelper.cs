using System;

namespace V308CMS.Helpers.Url
{
    public static class ImageUrlHelper
    {
        public static string ToUrl(this string path, int width =0, int height =0)
        {
            string imageUploadSource = System.Configuration.ConfigurationManager.AppSettings["ResourceDomain"] ?? String.Empty;
            if (imageUploadSource.Length < 1) {
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
            return imageUploadSource + "/" + resizeDir + "/" + imgUploadPath;
            //return ImageHelper.Crop(path, width,height);
            
        }
    }
}