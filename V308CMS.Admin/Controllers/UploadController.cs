using System;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;

namespace V308CMS.Admin.Controllers
{
    [CheckGroupPermission(false, "Upload file")]
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        public ActionResult Index()
        {
            var isSavedSuccessfully = true;
            var uploadPath = "";
            try
            {
                var listFiles = HttpContext.Request.Files;
                if (listFiles.Count > 0)
                {
                    foreach (string fileName in listFiles)
                    {
                        var file = listFiles[fileName];
                        if (file != null)
                        {
                             uploadPath = file.Upload();
                        }
                    }
                }
            }
            catch (Exception)
            {

                isSavedSuccessfully = false;
            }            
            if (isSavedSuccessfully)
            {
                return Json(new
                {
                    Message = uploadPath
                });
            }
            else
            {
                return Json(new
                {
                    Message = "Error in saving file"
                });
            }

        }
    }
}
