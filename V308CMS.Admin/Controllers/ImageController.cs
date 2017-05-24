using System;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class ImageController : BaseController
    {
        #region ANH
      
        [CheckAdminAuthorize(4)]
        public ActionResult Index(int? pType, int? pPage)
        {
            ImagesPage mImagesPage = new ImagesPage();
            string mLevel = "";
            if (pType == null)
            {
                if (Session["AnhType"] != null)
                    pType = (int)Session["AnhType"];
                else
                    pType = 0;
            }
            else
            {
                Session["AnhType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["AnhPage"] != null)
                    pPage = (int)Session["AnhPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["AnhPage"] = pPage;
            }
            #endregion
            //lay Level cua Type
            if (pType != 0)
            {
                var mImageType = ImagesService.LayTheLoaiAnhTheoId((int)pType);
                if (mImageType != null)
                    mLevel = mImageType.Level.Trim();
            }
            /*Lay danh sach cac tin theo page*/
            var mmImagesList = ImagesService.LayImageTheoTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
            var mImageTypeList = ImagesService.LayNhomAnhAll();
            if (mmImagesList.Count < 10)
                mImagesPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mImagesPage.Html = V308HTMLHELPER.TaoDanhSachCacAnh(mmImagesList, (int)pPage);
            mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnhHome(mImageTypeList, (int)pPage, (int)pType);
            mImagesPage.Page = (int)pPage;
            mImagesPage.TypeId = (int)pType;
            return View("Index", mImagesPage);
        }       
        [CheckAdminJson(4)]
        [HttpPost]
        [ActionName("Delete")]
        public JsonResult OnDelete(int pId = 0)
        {          
            var mImage = ImagesService.LayAnhTheoId(pId);
            if (mImage != null)
            {
                MpStartEntities.DeleteObject(mImage);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });

        }
       
        [CheckAdminAuthorize(4)]
        public ActionResult Create()
        {            
            ImagesPage mImagesPage = new ImagesPage();
            var mListImageType = ImagesService.LayNhomAnhAll();
            //Tao danh sach cac nhom tin
            mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnh4(mListImageType, 0);
            return View("Create", mImagesPage);
        }
        [HttpPost]       
        [CheckAdminJson(4)]
        [ValidateInput(false)]
        [ActionName("Create")]
        public JsonResult OnCreate(string pTitle, int? pGroupId, string pLink, string pSummary, string pImageUrl)
        {           
            var mImage = new Image()
            {
                Date = DateTime.Now,
                Link = pLink,
                LinkImage = pImageUrl,
                Name = pTitle,
                Summary = pSummary,
                TypeID = pGroupId
            };
            MpStartEntities.AddToImage(mImage);
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Lưu  ảnh thành công." });

        }       
        [CheckAdminAuthorize(4)]
        public ActionResult Edit(int pId = 0)
        {           
            ImagesPage mImagesPage = new ImagesPage();
            var mImage = ImagesService.LayAnhTheoId(pId);
            if (mImage != null)
            {
                var mImageTypeList = ImagesService.LayNhomAnhAll();
                //Tao danh sach cac nhom tin
                mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnh4(mImageTypeList, (int)mImage.TypeID);
                mImagesPage.pImage = mImage;
            }
            else
            {
                mImagesPage.Html = "Không tìm thấy tin tức cần sửa.";
            }
            return View("Edit", mImagesPage);
        }
        [HttpPost]       
        [CheckAdminJson(4)]
        [ValidateInput(false)]
        [ActionName("Edit")]
        public JsonResult OnEdit(int pId, string pTitle, int? pGroupId, string pLink, string pSummary, string pImageUrl)
        {
           
            var mImage = ImagesService.LayAnhTheoId(pId);
            if (mImage != null)
            {
                mImage.Name = pTitle;
                mImage.TypeID = pGroupId;
                mImage.Link = pLink;
                mImage.LinkImage = pImageUrl;
                mImage.Summary = pSummary;
                mImage.Date = DateTime.Now;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa ảnh thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy ảnh để sửa." });

        }       
    }
}