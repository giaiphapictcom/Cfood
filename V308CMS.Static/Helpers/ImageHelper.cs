using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace V308CMS.Static.Helpers
{
    public class ImageHelper
    {

        public static string checkFile(string filename) {
            var cachePath = HttpContext.Current.Server.MapPath("~/image-cache/");
            var filePath = cachePath + filename;
            string width = "";
            string height = "";
            if (!System.IO.File.Exists(filePath))
            {
                var dirPath = System.IO.Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                filename = Regex.Replace(filename,
                    @"/([0-9]+)x([0-9]+)/(.*?)",
                    delegate(Match match)
                    {
                        width = match.Groups[1].Value;
                        height = match.Groups[2].Value;

                        //e.QueryString["mode"] = "scale";
                        //return String.Format("/{0}x{1}/{2}", match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value);
                        return match.Groups[3].Value;
                });

                if (width.Length < 1 && height.Length < 1) {
                    filename = Regex.Replace(filename,
                    @"/w([0-9]+)h([0-9]+)/(.*?)",
                    delegate(Match match)
                    {
                        width = match.Groups[1].Value;
                        height = match.Groups[2].Value;
                        return match.Groups[3].Value;
                    });
                }
                if (width.Length < 1 && height.Length < 1)
                {
                    filename = Regex.Replace(filename,
                    @"/w([0-9]+)/(.*?)",
                    delegate(Match match)
                    {
                        width = match.Groups[1].Value;
                        return match.Groups[2].Value;
                    });
                }
                if (width.Length < 1 )
                {
                    filename = Regex.Replace(filename,
                    @"/h([0-9]+)/(.*?)",
                    delegate(Match match)
                    {
                        height = match.Groups[1].Value;
                        return match.Groups[2].Value;
                    });
                }
                filename = Crop(filename, width.Length > 0 ? int.Parse(width) : 0, height.Length > 0 ? int.Parse(height) : 0);
                
            }
            return "~/image-cache/" + filename;
            
        }

        public static string Crop(string img, int width = 0, int height = 0)
        {
            
            string ImageUploadSource = ConfigurationManager.AppSettings["ImageUploadSource"] ?? String.Empty;
            //var cachePath = String.Format("{0}{1}x{2}\\", HttpContext.Current.Server.MapPath("~/image-cache/"), width, height);
            var cachePath = HttpContext.Current.Server.MapPath("~/image-cache/");
            

            string imgRealPath = ImageUploadSource + "/" + img;
            string imgCachePath = cachePath + img;

            if (!System.IO.File.Exists(imgRealPath))
            {
                var filename = System.IO.Path.GetFileName(img);
                var img_empty = img.Replace(filename, "noimage.jpg");
                imgRealPath = cachePath + "noimage.jpg";

                imgCachePath = cachePath + img_empty;
                if (!System.IO.File.Exists(imgCachePath)) {
                    CropImage(width, height, imgRealPath, imgCachePath);
                }
                return img_empty;
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

            if (resizeDir.Length > 0)
            {
                cachePath += resizeDir + "\\";
            }

            imgCachePath = cachePath + img;
            CropImage(width, height, imgRealPath, imgCachePath);
            return resizeDir +"/"+ img;
        }

        static void CropImage(int Width, int Height, string sourceFilePath, string saveFilePath)
        {
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
            if (Width > 0) {
                percentageResizeW = ((float)Width / (float)sourceWidth);
            }
            
            
            if (Height > 0)
            {
                percentageResizeH = (float)Height / (float)sourceHeight;
            }

            if (percentageResizeH == 0 && percentageResizeW == 0) {
                percentageResizeH = 1;
                percentageResizeW = 1;
            }
            else if (percentageResizeH == 0) {
                percentageResizeH = percentageResizeW;
                percentageResize = percentageResizeW;
                Height = System.Convert.ToInt16(sourceHeight * percentageResize);
            }
            else if (percentageResizeW == 0)
            {
                percentageResizeW = percentageResizeH;
                percentageResize = percentageResizeH;
                Width = System.Convert.ToInt16(sourceWidth * percentageResize);
            }

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

            if (percentageResize == 0) {
                percentageResize = 1;
            }

            if (percentageResize >= 1) {
                System.IO.File.Copy(sourceFilePath, saveFilePath);
                return;
            }

            // Set the new cropped percentage image
            int destWidth = (int)Math.Round(sourceWidth * percentageResize);
            int destHeight = (int)Math.Round(sourceHeight * percentageResize);

            
           
            // Create the image object 
            try {
                var savePath = System.IO.Path.GetDirectoryName(saveFilePath);
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

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

                        switch (Path.GetExtension(sourceFilePath).ToLower())
                        {
                            case ".png":
                                objBitmap.Save(saveFilePath, ImageFormat.Png);
                                break;
                            case ".jpg":
                                objBitmap.Save(saveFilePath, ImageFormat.Jpeg);
                                break;
                            case ".gif":
                                objBitmap.Save(saveFilePath, ImageFormat.Gif);
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                System.IO.File.Copy(sourceFilePath, saveFilePath);
            }
            
        }

        
    }
}