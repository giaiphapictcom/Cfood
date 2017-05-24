using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class SupportController : BaseController
    {
        #region HO TRO
        
        [CheckAdminAuthorize(7)]
        public ActionResult Index(int? pPage)
        {           
            SupportPage mSupportPage = new SupportPage();
            if (pPage == null)
            {
                if (Session["HoTroPage"] != null)
                    pPage = (int)Session["HoTroPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["HoTroPage"] = pPage;
            }
            /*Lay danh sach cac tin theo page*/
            var mSupportList = SupportService.LayTheoTrang((int)pPage, 10);
            if (mSupportList.Count < 10)
                mSupportPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mSupportPage.Html = V308HTMLHELPER.TaoDanhSachSupport(mSupportList, (int)pPage);
            mSupportPage.Page = (int)pPage;
            return View("Index", mSupportPage);
        }       
        [CheckAdminJson(7)]
        [HttpPost]
        [ActionName("Delete")]
        public JsonResult OnDelete(int pId = 0)
        {           
            Support mSupport;
            mSupport = SupportService.LayTheoId(pId);
            if (mSupport != null)
            {
                MpStartEntities.DeleteObject(mSupport);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy kiểu cần xóa." });
        }       
        [CheckAdminAuthorize(7)]
        public ActionResult Create()
        {          
            SupportPage mSupportPage = new SupportPage();
            var mSupportType = SupportService.LaySupportTypeAll();
            mSupportPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachSupportType2(mSupportType, 0);
            return View("Create", mSupportPage);
        }
        [HttpPost]       
        [CheckAdminJson(7)]
        [ValidateInput(false)]
        [ActionName("Create")]
        public JsonResult OnCreate(string pTieuDe, int? pUuTien, int? pGroupId, string pNick, string pMobile, string pEmail)
        {                    
            var mSupport = new Support() { Date = DateTime.Now, Name = pTieuDe, TypeID = pGroupId, Phone = pMobile, Nick = pNick, Email = pEmail };
            MpStartEntities.AddToSupport(mSupport);
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Lưu loại hỗ trợ thành công." });

        }      
        [CheckAdminAuthorize(7)]
        public ActionResult Edit(int pId = 0)
        {          
            SupportPage mSupportPage = new SupportPage();
            var mSupport = SupportService.LayTheoId(pId);
            if (mSupport != null)
            {
                var mSupportType = SupportService.LaySupportTypeAll();
                mSupportPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachSupportType3(mSupportType, (int)mSupport.TypeID);
                mSupportPage.pSupport = mSupport;
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
        public JsonResult OnEdit(int pId, string pTieuDe, int? pUuTien, int? pGroupId, string pNick, string pMobile, string pEmail)
        {
            var mSupport = SupportService.LayTheoId(pId);
            if (mSupport != null)
            {
                mSupport.Name = pTieuDe;
                mSupport.Date = DateTime.Now;
                mSupport.TypeID = pGroupId;
                mSupport.Nick = pNick;
                mSupport.Phone = pMobile;
                mSupport.Email = pEmail;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa thể hỗ trợ thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy hỗ trợ để sửa." });

        }
        #endregion
    }
}