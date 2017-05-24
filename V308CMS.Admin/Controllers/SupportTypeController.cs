using System;
using System.Collections.Generic;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class SupportTypeController : BaseController
    {
        #region LOAI HO TRO
      
        [CheckAdminAuthorize(7)]
        public ActionResult Index(int? pPage)
        {           
            SupportPage mSupportPage = new SupportPage();
            if (pPage == null)
            {
                if (Session["LoaiHoTroPage"] != null)
                    pPage = (int)Session["LoaiHoTroPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["LoaiHoTroPage"] = pPage;
            }
            #endregion
            /*Lay danh sach cac tin theo page*/
            var mSupportTypeList = SupportService.LaySupportTypeTheoTrang((int)pPage, 10);
            if (mSupportTypeList.Count < 10)
                mSupportPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mSupportPage.Html = V308HTMLHELPER.TaoDanhSachSupportType(mSupportTypeList, (int)pPage);
            mSupportPage.Page = (int)pPage;
            return View("Index", mSupportPage);
        }
      
        [CheckAdminJson(7)]
        [HttpPost]
        [ActionName("Delete")]
        public JsonResult OnDelete(int pId = 0)
        {
            var mSupportType = SupportService.LaySupportTypeTheoId(pId);
            if (mSupportType != null)
            {
                MpStartEntities.DeleteObject(mSupportType);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy kiểu cần xóa." });
        }
     
        [CheckAdminAuthorize(7)]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        [CheckAdminJson(7)]
        [ValidateInput(false)]
        [ActionName("Create")]
        public JsonResult OnCreate(string pTieuDe, int? pUuTien)
        {            
            var mSupportType = new SupportType() { Date = DateTime.Now, Number = pUuTien, Name = pTieuDe };
            MpStartEntities.AddToSupportType(mSupportType);
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Lưu loại hỗ trợ thành công." });

        }
        [CheckAdminAuthorize(7)]
        public ActionResult Edit(int pId = 0)
        {          
            SupportPage mSupportPage = new SupportPage();
            var mSupportType = SupportService.LaySupportTypeTheoId(pId);
            if (mSupportType != null)
            {
                mSupportPage.pSupportType = mSupportType;
            }
            else
            {
                mSupportPage.Html = "Không tìm thấy tin tức cần sửa.";
            }
            return View("Edit", mSupportPage);
        }
        [HttpPost]
        [CheckAdminJson(7)]
        [ValidateInput(false)]
        [ActionName("Edit")]
        public JsonResult OnEdit(int pId, string pTieuDe, int? pUuTien)
        {
            var mSupportType = SupportService.LaySupportTypeTheoId(pId);
            if (mSupportType != null)
            {
                mSupportType.Name = pTieuDe;
                mSupportType.Date = DateTime.Now;
                mSupportType.Number = pUuTien;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa thể loại hỗ trợ thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy hỗ trợ để sửa." });

        }        
    }
}