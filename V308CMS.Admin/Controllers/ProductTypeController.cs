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
    public class ProductTypeController : BaseController
    {
        #region LOAI SAN PHAM
        [CheckAdminAuthorize(1)]
        public ActionResult Index(int? pType, int? pPage)
        {           
            ProductPage mProductPage = new ProductPage();
            ProductType mProductTypeDetail = new ProductType() { Parent = 0 };
            string mLevel = "";
            if (pType == null)
            {
                if (Session["LoaiSanPhamType"] != null)
                    pType = (int)Session["LoaiSanPhamType"];
                else
                    pType = 0;
            }
            else
            {
                Session["LoaiSanPhamType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["LoaiSanPhamPage"] != null)
                    pPage = (int)Session["LoaiSanPhamPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["LoaiSanPhamPage"] = pPage;
            }
            #endregion
            //lay Level cua Type
            if (pType != 0)
            {
                mProductTypeDetail = ProductsService.LayProductTypeTheoId((int)pType);
                if (mProductTypeDetail != null)
                    mLevel = mProductTypeDetail.Level.Trim();
            }
            /*Lay danh sach cac tin theo page*/
            /*Lay danh sach cac tin theo page*/
            var mProductType = ProductsService.LayProductTypeTheoTrangAndType((int)pPage, 5, (int)pType, mLevel);
            //Lay tat ca cac nhom
            var mProductTypeAll = ProductsService.LayProductTypeAll();
            var mProductTypeChildList = ProductsService.LayProductTypeTheoParentId((int)pType);
            if (mProductType.Count < 5)
                mProductPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mProductPage.Html = V308HTMLHELPER.TaoDanhSachProductType(mProductType, (int)pPage);
            mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductTypeHome2(mProductTypeAll, (int)pPage, (int)pType);
            mProductPage.Page = (int)pPage;
            mProductPage.TypeId = (int)pType;
            mProductPage.ProductTypeLt = mProductTypeChildList;
            mProductPage.pProductType = mProductTypeDetail;
            return View("Index", mProductPage);
        }     
        [CheckAdminJson(1)]
        [HttpPost]       
        public JsonResult OnDelete(int pId = 0)
        {           
            var mProductType = ProductsService.LayProductTypeTheoId(pId);
            if (mProductType != null)
            {
                MpStartEntities.DeleteObject(mProductType);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });

        }     
        [CheckAdminAuthorize(1)]
        public ActionResult Create()
        {           
            ProductPage mProductPage = new ProductPage();
            var mProductType = ProductsService.LayProductTypeAll();
            //Tao danh sach cac nhom tin
            mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductType2(mProductType, 0);
            return View("Create", mProductPage);
        }
        [HttpPost]      
        [CheckAdminJson(1)]
        [ValidateInput(false)]      
        public JsonResult OnCreate(string pTieuDe, int? pGroupId, string pSummary, int? pKichHoat, int? pUuTien, string pImageUrl, string pImageBanner, string pDescription)
        {         
            ProductType mProductType;
            string[] mLevelArray;
            int mLevel = 0;
            if (pGroupId == 0)
            {
                //Tinh gia tri Level moi cho Group nay
                //1- Lay tat ca cac Group me
                //2- Convert gia tri Level de lay gia tri lon nhat
                //3- Tao gia tri moi lon hon gia tri lon nhat
                mLevelArray = (from p in MpStartEntities.ProductType
                               where p.Parent == 0
                               select p.Level).ToArray();
                mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                mLevel = (mLevel + 1);
                mProductType = new ProductType() { Date = DateTime.Now, Number = pUuTien, Level = mLevel.ToString(), Status = true, Name = pTieuDe, Parent = pGroupId, Detail = pSummary, Image = pImageUrl, ImageBanner = pImageBanner, Description = pDescription };
                MpStartEntities.AddToProductType(mProductType);
                MpStartEntities.SaveChanges();
            }
            else
            {
                //lay level cua nhom me
                var mProductTypeParent = ProductsService.LayProductTypeTheoId((int)pGroupId);
                if (mProductTypeParent != null)
                {
                    mLevelArray = (from p in MpStartEntities.ProductType
                                   where (p.Level.Substring(0, mProductTypeParent.Level.Length).Equals(mProductTypeParent.Level)) && (p.Level.Length == (mProductTypeParent.Level.Length + 5))
                                   select p.Level).ToArray();
                    if (mLevelArray.Any())
                    {
                        mLevel = mLevelArray.Select(p => Convert.ToInt32(p)).ToArray().Max();
                        mLevel = (mLevel + 1);
                    }
                    else
                    {
                        mLevel = Convert.ToInt32(mProductTypeParent.Level.ToString().Trim() + "10001");
                    }
                    mProductType = new ProductType() { Date = DateTime.Now, Number = pUuTien, Level = mLevel.ToString(), Status = true, Name = pTieuDe, Parent = pGroupId, Detail = pSummary, Image = pImageUrl, Description = pDescription };
                    MpStartEntities.AddToProductType(mProductType);
                    MpStartEntities.SaveChanges();
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy nhóm sản phẩm." });
                }
            }
            return Json(new { code = 1, message = "Lưu loại sản phẩm thành công." });

        }     
        [CheckAdminAuthorize(1)]
        public ActionResult Edit(int pId = 0)
        {            
            ProductPage mProductPage = new ProductPage();
            var mProductType = ProductsService.LayProductTypeTheoId(pId);
            if (mProductType != null)
            {
                var mListProductType = ProductsService.LayProductTypeAll();
                //Tao danh sach cac nhom tin
                mProductPage.HtmlNhom = V308HTMLHELPER.TaoDanhSachProductType3(mListProductType, (int)mProductType.Parent);
                mProductPage.pProductType = mProductType;
            }
            else
            {
                mProductPage.Html = "Không tìm thấy tin tức cần sửa.";
            }
            return View("Edit", mProductPage);
        }
        [HttpPost]      
        [CheckAdminJson(1)]
        [ValidateInput(false)]       
        public JsonResult OnEdit(int pId, string pTieuDe, int? pGroupId, string pSummary, int? pKichHoat, int? pUuTien, string pImageUrl, string pImageBanner, string pDescription)
        {            
            var mProductType = ProductsService.LayProductTypeTheoId(pId);
            if (mProductType != null)
            {
                mProductType.Name = pTieuDe;
                mProductType.Date = DateTime.Now;
                mProductType.Number = pUuTien;
                mProductType.Parent = pGroupId;
                mProductType.Detail = pSummary;
                mProductType.Image = pImageUrl;
                mProductType.ImageBanner = pImageBanner;
                mProductType.Description = pDescription;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa loại sản phẩm thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy loại sản phẩm để sửa." });

        }
    }
}