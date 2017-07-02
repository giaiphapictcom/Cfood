using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Configuration;

namespace V308CMS.Common
{
    public class ImageHelper
    {
        public static string ToUrl(string path, int width = 0, int height = 0)
        {
            string ImageUploadSource = System.Configuration.ConfigurationManager.AppSettings["ResourceDomain"] ?? String.Empty;
            if (ImageUploadSource.Length < 1)
            {
                return path;
            }
            else if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
            {
                return path;
            }

            path = path.Replace("\\Content\\Images\\", "");
            path = path.Replace("/Content/Images/", "");

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
            path = path.Replace("\\", "/");
            var imgUploadPath = path.Replace("/Content/Images/", "").Trim();

            if (imgUploadPath.Length < 1)
            {
                imgUploadPath = "noimage.jpg";
            }
            return ImageUploadSource + "/" + resizeDir + "/" + imgUploadPath;


        }

        public static string Crop(string img,int width = 100,int height=0)
        {
            string imgPath = "/Content/Images/noimage.jpg";
            if (width < 1) {
                width = 100;
            }
            if (height < 1) {
                height = width;
            }

            string extension = Path.GetExtension(img);
            string filenameEncode = Base64Encode(img) + extension;

            var appSettings = ConfigurationManager.AppSettings;
            string uploadPath = appSettings["imgUpload"];
            string imgThumbPath = appSettings["imgThumbPath"];
            string imgThumbUrl = appSettings["imgThumbUrl"];

            if (uploadPath.Count() < 1 | !Directory.Exists(imgThumbPath))
            {
                //imgThumbPath = HttpContext.Current.Server.MapPath("~/Content/thumb/");
                imgThumbUrl = "/Content/thumb/";
            }
            string thumbPath = imgThumbPath + "/" + width+"x"+height + "/";
            if (!Directory.Exists(thumbPath))
            {
                Directory.CreateDirectory(thumbPath);
            }

            if (System.IO.File.Exists(thumbPath + filenameEncode)) {
                return imgThumbUrl + width + "x" + height + "/" + filenameEncode;
            }

            if (uploadPath.Count() > 0 & Directory.Exists(uploadPath))
            {
                string imgRealPath = uploadPath + img;
                if (System.IO.File.Exists(imgRealPath))
                {
                    extension = Path.GetExtension(imgRealPath);
                    if (!System.IO.File.Exists(thumbPath + filenameEncode))
                    {
                        CropImage(width, height, imgRealPath, thumbPath + filenameEncode);
                        imgPath = imgThumbUrl + width + "x" + height + "/" + filenameEncode;
                    }
                }
                else {
                    imgRealPath = HttpContext.Current.Server.MapPath("~") + img;
                    if (System.IO.File.Exists(imgRealPath))
                    {
                        extension = Path.GetExtension(imgRealPath);

                        if (!System.IO.File.Exists(thumbPath + filenameEncode))
                        {
                            CropImage(width, height, imgRealPath, thumbPath + filenameEncode);
                            imgPath = imgThumbUrl + width + "x" + height + "/" + filenameEncode;
                        }
                    }
                }


            }
            return imgPath;
        }

        static void CropImage(int Width, int Height, string sourceFilePath, string saveFilePath)
        {
            if (saveFilePath.Length > 248)
                return;
            // variable for percentage resize 
            float percentageResize = 0;
            float percentageResizeW = 0;
            float percentageResizeH = 0;

            // variables for the dimension of source and cropped image 
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // Create a bitmap object file from source file 
            Bitmap sourceImage = new Bitmap(sourceFilePath);

            // Set the source dimension to the variables 
            int sourceWidth = sourceImage.Width;
            int sourceHeight = sourceImage.Height;

            // Calculate the percentage resize 
            percentageResizeW = ((float)Width / (float)sourceWidth);
            percentageResizeH = ((float)Height / (float)sourceHeight);

            // Checking the resize percentage 
            if (percentageResizeH < percentageResizeW)
            {
                percentageResize = percentageResizeW;
                destY = System.Convert.ToInt16((Height - (sourceHeight * percentageResize)) / 2);
            }
            else
            {
                percentageResize = percentageResizeH;
                destX = System.Convert.ToInt16((Width - (sourceWidth * percentageResize)) / 2);
            }

            // Set the new cropped percentage image
            int destWidth = (int)Math.Round(sourceWidth * percentageResize);
            int destHeight = (int)Math.Round(sourceHeight * percentageResize);

            // Create the image object 
            using (Bitmap objBitmap = new Bitmap(Width, Height))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    // Set the graphic format for better result cropping 
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

                    // Save the file path, note we use png format to support png file 
                    objBitmap.Save(saveFilePath, ImageFormat.Png);
                }
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}