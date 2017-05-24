using System;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class FileTypeController : BaseController
    {
        #region THE LOAI FILE
        
        [CheckAdminAuthorize(5)]
        public ActionResult Index(int? pType, int? pPage)
        {           
            FilePage mFilePage = new FilePage();
            string mLevel = "";
            if (pType == null)
            {
                if (Session["LoaiFileType"] != null)
                    pType = (int)Session["LoaiFileType"];
                else
                    pType = 0;
            }
            else
            {
                Session["LoaiFileType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["LoaiFilePage"] != null)
                    pPage = (int)Session["LoaiFilePage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["LoaiFilePage"] = pPage;
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
            var mFileTypeAll = FileService.LayNhomFileAll();
            /*Lay danh sach cac tin theo page*/
            var mmFileList = FileService.LayFileTypeTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
            if (mmFileList.Count < 10)
                mFilePage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mFilePage.Html = V308HTMLHELPER.TaoDanhSachCacNhomFile(mmFileList, (int)pPage);
            mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFileHome2(mFileTypeAll, (int)pPage, (int)pType);
            mFilePage.Page = (int)pPage;
            mFilePage.TypeId = (int)pType;
            return View("Index", mFilePage);
        }       
        [CheckAdminJson(5)]
        [HttpPost]
        [ActionName("Delete")]
        public JsonResult OnDelete(int pId = 0)
        {           
            var mFileType = FileService.LayTheLoaiFileTheoId(pId);
            if (mFileType != null)
            {
                MpStartEntities.DeleteObject(mFileType);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });

        }
     
        [CheckAdminAuthorize(5)]
        public ActionResult Create()
        {           
            FilePage mFilePage = new FilePage();
            var mListFileType = FileService.LayNhomFileAll();
            //Tao danh sach cac nhom tin
            mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFile2(mListFileType, 0);
            return View("Create", mFilePage);
        }
        [HttpPost]       
        [CheckAdminJson(5)]
        [ValidateInput(false)]
        [ActionName("Create")]
        public JsonResult OnCreate(string pTieuDe, int? pGroupId, int? pUuTien, string pKichCo, string pImageUrl)
        {          
            FileType mFileType;
            string[] mLevelArray;
            var mLevel = 0;
            if (pGroupId == 0)
            {
                //Tinh gia tri Level moi cho Group nay
                //1- Lay tat ca cac Group me
                //2- Convert gia tri Level de lay gia tri lon nhat
                //3- Tao gia tri moi lon hon gia tri lon nhat
                mLevelArray = (from p in MpStartEntities.FileType
                               where p.ParentID == 0
                               select p.Level).ToArray();
                mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                mLevel = (mLevel + 1);
                mFileType = new FileType() { Date = DateTime.Now, Level = mLevel.ToString(), Number = pUuTien, Name = pTieuDe, ParentID = pGroupId };
                MpStartEntities.AddToFileType(mFileType);
                MpStartEntities.SaveChanges();
            }
            else
            {
                //lay level cua nhom me
                var mFileTypeParent = FileService.LayTheLoaiFileTheoId((int)pGroupId);
                if (mFileTypeParent != null)
                {
                    mLevelArray = (from p in MpStartEntities.NewsGroups
                                   where (p.Level.Substring(0, mFileTypeParent.Level.Length).Equals(mFileTypeParent.Level)) && (p.Level.Length == (mFileTypeParent.Level.Length + 5))
                                   select p.Level).ToArray();
                    if (mLevelArray.Any())
                    {
                        mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                        mLevel = (mLevel + 1);
                    }
                    else
                    {
                        mLevel = Convert.ToInt32(mFileTypeParent.Level.ToString().Trim() + "10001");
                    }
                    mFileType = new FileType() { Date = DateTime.Now, Level = mLevel.ToString(), Number = pUuTien, Name = pTieuDe, ParentID = pGroupId };
                    MpStartEntities.AddToFileType(mFileType);
                    MpStartEntities.SaveChanges();
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy nhóm file." });
                }
            }
            return Json(new { code = 1, message = "Lưu loại file thành công." });

        }       
        [CheckAdminAuthorize(5)]
        public ActionResult Edit(int pId = 0)
        {            
            FilePage mFilePage = new FilePage();
            var mFileType = FileService.LayTheLoaiFileTheoId(pId);
            if (mFileType != null)
            {
                var mFileTypeList = FileService.LayNhomFileAll();
                //Tao danh sach cac nhom tin
                mFilePage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomFile3(mFileTypeList, (int)mFileType.ParentID);
                mFilePage.pFileType = mFileType;
            }
            else
            {
                mFilePage.Html = "Không tìm thấy loại file cần sửa.";
            }
            return View("Edit", mFilePage);
        }
        [HttpPost]        
        [CheckAdminJson(5)]
        [ValidateInput(false)]
        [ActionName("Edit")]
        public JsonResult OnEdit(int pId, string pTieuDe, int? pGroupId, int? pUuTien, string pKichCo, string pImageUrl)
        {           
            var mFileType = FileService.LayTheLoaiFileTheoId(pId);
            if (mFileType != null)
            {
                mFileType.Name = pTieuDe;
                mFileType.Date = DateTime.Now;
                mFileType.Number = pUuTien;
                mFileType.ParentID = pGroupId;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa thể loại File thành công." });
            }
            else
            {
                return Json(new { code = 0, message = "Không tìm thấy loại File để sửa." });
            }

        }      
    }
}