using System;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class ImageTypeController : BaseController
    {
        #region THE LOAI ANH
      
        [CheckAdminAuthorize(4)]
        public ActionResult Index(int? pType, int? pPage)
        {
            ImagesPage mImagesPage = new ImagesPage();
            string mLevel = "";
            if (pType == null)
            {
                if (Session["LoaiAnhType"] != null)
                    pType = (int)Session["LoaiAnhType"];
                else
                    pType = 0;
            }
            else
            {
                Session["LoaiAnhType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["LoaiAnhPage"] != null)
                    pPage = (int)Session["LoaiAnhPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["LoaiAnhPage"] = pPage;
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
            var mImageTypeAll = ImagesService.LayNhomAnhAll();
            /*Lay danh sach cac tin theo page*/
            var mmImagesList = ImagesService.LayImageTypeTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
            if (mmImagesList.Count < 10)
                mImagesPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mImagesPage.Html = V308HTMLHELPER.TaoDanhSachCacNhomAnh(mmImagesList, (int)pPage);
            mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnhHome2(mImageTypeAll, (int)pPage, (int)pType);
            mImagesPage.Page = (int)pPage;
            mImagesPage.TypeId = (int)pType;
            return View("Index", mImagesPage);
        }      
        [CheckAdminJson(4)]
        [HttpPost]        
        public JsonResult OnDelete(int pId = 0)
        {           
            var mImageType = ImagesService.LayTheLoaiAnhTheoId(pId);
            if (mImageType != null)
            {
                MpStartEntities.DeleteObject(mImageType);
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
            mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnh2(mListImageType, 0);
            return View("Create", mImagesPage);
        }
        [HttpPost]     
        [CheckAdminJson(4)]
        [ValidateInput(false)]       
        public JsonResult OnCreate(string pTieuDe, int? pGroupId, int? pUuTien, string pKichCo, string pImageUrl)
        {          
            ImageType mImageType;
            string[] mLevelArray;
            var mLevel = 0;
            if (pGroupId == 0)
            {
                //Tinh gia tri Level moi cho Group nay
                //1- Lay tat ca cac Group me
                //2- Convert gia tri Level de lay gia tri lon nhat
                //3- Tao gia tri moi lon hon gia tri lon nhat
                mLevelArray = (from p in MpStartEntities.ImageType
                               where p.Parent == 0
                               select p.Level).ToArray();
                mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                mLevel = (mLevel + 1);
                mImageType = new ImageType() { Date = DateTime.Now, Level = mLevel.ToString(), Number = pUuTien, Name = pTieuDe, Parent = pGroupId, Size = pKichCo, Image = pImageUrl };
                MpStartEntities.AddToImageType(mImageType);
                MpStartEntities.SaveChanges();
            }
            else
            {
                //lay level cua nhom me
                var mImageTypeParent = ImagesService.LayTheLoaiAnhTheoId((int)pGroupId);
                if (mImageTypeParent != null)
                {
                    mLevelArray = (from p in MpStartEntities.NewsGroups
                                   where (p.Level.Substring(0, mImageTypeParent.Level.Length).Equals(mImageTypeParent.Level)) && (p.Level.Length == (mImageTypeParent.Level.Length + 5))
                                   select p.Level).ToArray();
                    if (mLevelArray.Any())
                    {
                        mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                        mLevel = (mLevel + 1);
                    }
                    else
                    {
                        mLevel = Convert.ToInt32(mImageTypeParent.Level.ToString().Trim() + "10001");
                    }
                    mImageType = new ImageType() { Date = DateTime.Now, Level = mLevel.ToString(), Number = pUuTien, Name = pTieuDe, Parent = pGroupId, Size = pKichCo, Image = pImageUrl };
                    MpStartEntities.AddToImageType(mImageType);
                    MpStartEntities.SaveChanges();
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy nhóm ảnh." });
                }
            }
            return Json(new { code = 1, message = "Lưu loại ảnh thành công." });

        }       
        [CheckAdminAuthorize(4)]
        public ActionResult Edit(int pId = 0)
        {                        
            ImagesPage mImagesPage = new ImagesPage();
            var mImageType = ImagesService.LayTheLoaiAnhTheoId(pId);
            if (mImageType != null)
            {
                var mImageTypeList = ImagesService.LayNhomAnhAll();
                //Tao danh sach cac nhom tin
                mImagesPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachNhomAnh3(mImageTypeList, (int)mImageType.Parent);
                mImagesPage.pImageType = mImageType;
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
        public JsonResult OnEdit(int pId, string pTieuDe, int? pGroupId, int? pUuTien, string pKichCo, string pImageUrl)
        {                 
            var mImageType = ImagesService.LayTheLoaiAnhTheoId(pId);
            if (mImageType != null)
            {
                mImageType.Name = pTieuDe;
                mImageType.Date = DateTime.Now;
                mImageType.Number = pUuTien;
                mImageType.Parent = pGroupId;
                mImageType.Size = pKichCo;
                mImageType.Image = pImageUrl;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa thể loại ảnh thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy loại ảnh để sửa." });

        }      
    }
}