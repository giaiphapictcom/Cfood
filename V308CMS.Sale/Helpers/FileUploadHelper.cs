using System;
using System.IO;
using System.Linq;
using System.Web;

namespace V308CMS.Sale.Helpers
{
    public static class ImageUploadHelper
    {
        private static readonly string[] ValidImageType = { "image/gif", "image/jpeg", "image/jpg", "image/png" };
        private static readonly string BasePath = ConfigHelper.UploadFolder;
        private static readonly  string VirtualBasePath = ConfigHelper.VirtualUploadPath;

        private static readonly int MaxSize = ConfigHelper.MaxImageSize;

        public static string Upload(this HttpPostedFileBase imageUpoad)
        {
            var uploadVirtualPath = string.Empty;
            if (imageUpoad != null && (imageUpoad.ContentLength > 0 
                && imageUpoad.ContentLength < MaxSize) && 
                ValidImageType.Contains(imageUpoad.ContentType))
            {
                var dayDirName = DateTime.Now.ToString("ddMMyyyy");
                var uploadDir = BasePath + dayDirName;
                uploadVirtualPath = VirtualBasePath + dayDirName;
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir.Contains(":")
                        ? uploadDir
                        : HttpContext.Current.Server.MapPath(uploadDir));
                }
                var imagePath = Path.Combine(uploadDir.Contains(":") ? 
                    uploadDir : 
                    HttpContext.Current.Server.MapPath(uploadDir), imageUpoad.FileName);

                if (!File.Exists(imagePath))
                {
                    imageUpoad.SaveAs(imagePath);
                }
                uploadVirtualPath = Path.Combine(uploadVirtualPath, imageUpoad.FileName);
            }
            return uploadVirtualPath;

        }

    }
}