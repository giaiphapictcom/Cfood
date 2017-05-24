using System;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class FileController : BaseController
    {
        // GET: File
        #region FILE
      
        [CheckAdminAuthorize(5)]
        public ActionResult Index(int? pType, int? pPage)
        {
          
            FilePage mFilePage = new FilePage();
            string mLevel = "";
            if (pType == null)
            {
                if (Session["FileType"] != null)
                    pType = (int)Session["FileType"];
                else
                    pType = 0;
            }
            else
            {
                Session["FileType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["FilePage"] != null)
                    pPage = (int)Session["FilePage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["FilePage"] = pPage;
            }
            #endregion
            //lay Level cua Type
            if (pType != 0)
            {
                var mFileType = FileService.LayTheLoaiFileTheoId((int)pType);
                if (mFileType != null)
                    mLevel = mFileType.Level.Trim();
            }
            /*Lay danh sach cac tin theo page*/
            var mmFileList = FileService.LayFileTheoTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
            var mFileTypeList = FileService.LayNhomFileAll();
            if (mmFileList.Count < 10)
                mFilePage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mFilePage.Html = V308HTMLHELPER.TaoDanhSachCacFile(mmFileList, (int)pPage);
            mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFileHome(mFileTypeList, (int)pPage, (int)pType);
            mFilePage.Page = (int)pPage;
            mFilePage.TypeId = (int)pType;
            return View("Index", mFilePage);
        }      
        [CheckAdminJson(5)]
        [HttpPost]
        [ActionName("Delete")]
        public JsonResult OnDelete(int pId = 0)
        {
            
            var mFile = FileService.LayFileTheoId(pId);
            if (mFile != null)
            {
                MpStartEntities.DeleteObject(mFile);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy File cần xóa." });

        }     
        [CheckAdminAuthorize(5)]
        public ActionResult Create()
        {           
           
            var mFilePage = new FilePage();
            var mListFileType = FileService.LayNhomFileAll();
            //Tao danh sach cac nhom tin
            mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFile4(mListFileType, 0);
            return View("Create", mFilePage);
        }
        [HttpPost]     
        [CheckAdminJson(5)]
        [ValidateInput(false)]
        [ActionName("Create")]
        public JsonResult OnCreate(string pTitle, int? pGroupId, string pLink, string pKichHoat, int? pValue, string pSummary, string pImageUrl)
        {           
            var mFile = new File()
            {
                Date = DateTime.Now,
                LinkFile = pImageUrl,
                Status = ConverterUlti.ConvertStringToLogic2(pKichHoat),
                FileName = pTitle,
                Summary = pSummary,
                TypeID = pGroupId,
                Value = pValue
            };
            MpStartEntities.AddToFile(mFile);
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Lưu  File thành công." });

        }      
        [CheckAdminAuthorize(5)]
        public ActionResult Edit(int pId = 0)
        {          
            FilePage mFilePage = new FilePage();
            var mFile = FileService.LayFileTheoId(pId);
            if (mFile != null)
            {
                var mFileTypeList = FileService.LayNhomFileAll();
                //Tao danh sach cac nhom tin
                mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFile4(mFileTypeList, (int)mFile.TypeID);
                mFilePage.pFile = mFile;
            }
            else
            {
                mFilePage.Html = "Không tìm thấy File cần sửa.";
            }
            return View("Edit", mFilePage);
        }
        [HttpPost]    
        [CheckAdminJson(5)]
        [ValidateInput(false)]
        [ActionName("Edit")]
        public JsonResult OnEdit(int pId, string pTitle, int? pGroupId, int? pValue, string pLink, string pKichHoat, string pSummary, string pImageUrl)
        {           
            var mFile = FileService.LayFileTheoId(pId);
            if (mFile != null)
            {
                mFile.FileName = pTitle;
                mFile.TypeID = pGroupId;
                mFile.LinkFile = pImageUrl;
                mFile.Summary = pSummary;
                mFile.Date = DateTime.Now;
                mFile.Value = pValue;
                mFile.Status = ConverterUlti.ConvertStringToLogic2(pKichHoat);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa File thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy File để sửa." });

        }
      
    }
}