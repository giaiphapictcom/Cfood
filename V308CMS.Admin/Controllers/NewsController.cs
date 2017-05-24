using System;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class NewsController : BaseController
    {
        [CheckAdminAuthorize(2)]
        public ActionResult Index(int? pType, int? pPage)
        {
            NewsPage mNewsPage = new NewsPage();
            string mLevel = "";
            if (pType == null)
            {
                if (Session["TinTucType"] != null)
                    pType = (int)Session["TinTucType"];
                else
                    pType = 0;
            }
            else
            {
                Session["TinTucType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["TinTucPage"] != null)
                    pPage = (int)Session["TinTucPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["TinTucPage"] = pPage;
            }
            //lay Level cua Type
            if (pType != 0)
            {
                var mNewsGroups = NewsService.LayTheLoaiTinTheoId((int)pType);
                if (mNewsGroups != null)
                    mLevel = mNewsGroups.Level.Trim();
            }
            /*Lay danh sach cac tin theo page*/
            var mNewsList = NewsService.LayTinTheoTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
            var mNewsGroupsList = NewsService.LayNhomTinAll();
            if (mNewsList.Count < 10)
                mNewsPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mNewsPage.Html = V308HTMLHELPER.TaoDanhSachTinTuc(mNewsList, (int)pPage);
            //Tao danh sach cac nhom tin
            mNewsPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTinHome(mNewsGroupsList, (int)pPage, (int)pType);
            mNewsPage.Page = (int)pPage;
            mNewsPage.TypeId = (int)pType;
            return View(mNewsPage);
        }

        [CheckAdminJson(2)]
        [HttpPost]     
        public JsonResult OnDelete(int pId = 0)
        {
            var mNews = NewsService.LayTinTheoId(pId);
            if (mNews != null)
            {
                MpStartEntities.DeleteObject(mNews);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });
        }
        [CheckAdminJson(2)]
        [HttpPost]      
        public JsonResult OnChangeStatus(int pId = 0)
        {           
            var mNews = NewsService.LayTinTheoId(pId);
            if (mNews != null)
            {
                mNews.Status = !mNews.Status;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = (mNews.Status == true? "Hiển thị thành công !":"Ẩn thành công!") });
            }
            return Json(new { code = 0, message = "Không tìm thấy tin cần ẩn." });

        }
        [CheckAdminAuthorize(2)]
        public ActionResult Create()
        {           
            NewsPage mNewsPage = new NewsPage();
            var mListNewsGroup = NewsService.LayNhomTinAll();
            //Tao danh sach cac nhom tin
            mNewsPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTin(mListNewsGroup, 0);
            return View(mNewsPage);
        }
        [HttpPost]
        [CustomAuthorize]
        [CheckAdminJson(2)]
        [ValidateInput(false)]
        public JsonResult OnCreate
            (
                string pTieuDe, string pImageUrl,
                int? pGroupId, string pMoTa, 
                string pNoiDung, string pKichHoat, 
                int? pUuTien, string pDescription, string pKeyWord, 
                string pSlide, string pHot, string pFast,
                string pFeatured
            )
        {
            var mNews = new News() { Date = DateTime.Now, Detail = pNoiDung, Image = pImageUrl, Order = pUuTien, Status = true, Summary = pMoTa, Title = pTieuDe, TypeID = pGroupId, Keyword = pKeyWord, Description = pDescription, Slider = ConverterUlti.ConvertStringToLogic2(pSlide), Hot = ConverterUlti.ConvertStringToLogic2(pHot), Fast = ConverterUlti.ConvertStringToLogic2(pFast), Featured = ConverterUlti.ConvertStringToLogic2(pFeatured) };
            MpStartEntities.AddToNews(mNews);
            MpStartEntities.SaveChanges();
            return Json(new { code = 1, message = "Lưu tin tức thành công." });

        }
        [CheckAdminAuthorize(2)]
        public ActionResult Edit(int pId = 0)
        {
            NewsPage mNewsPage = new NewsPage();
            var mNews = NewsService.LayTinTheoId(pId);
            if (mNews != null)
            {
                var mListNewsGroup = NewsService.LayNhomTinAll();
                //Tao danh sach cac nhom tin
                mNewsPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTin(mListNewsGroup, (int)mNews.TypeID);
                mNewsPage.pNews = mNews;
            }
            else
            {
                mNewsPage.Html = "Không tìm thấy tin tức cần sửa.";
            }
            return View(mNewsPage);
        }
        [HttpPost]
        [CheckAdminJson(2)]
        [ValidateInput(false)]     
        public JsonResult OnEdit
            (
                int pId, string pTieuDe, string pImageUrl,
                int? pGroupId, string pMoTa, string pNoiDung,
                string pKichHoat, int? pUuTien, string pDescription,
                string pKeyWord, string pSlide, string pHot,
                string pFast, string pFeatured
            )
        {
            var mNews = NewsService.LayTinTheoId(pId);
            if (mNews != null)
            {
                mNews.Title = pTieuDe;
                mNews.Date = DateTime.Now;
                mNews.Detail = pNoiDung;
                mNews.Image = pImageUrl;
                mNews.Order = pUuTien;
                mNews.Summary = pMoTa;
                mNews.Keyword = pKeyWord;
                mNews.Description = pDescription;
                mNews.TypeID = pGroupId;
                mNews.Status = ConverterUlti.ConvertStringToLogic2(pKichHoat);
                mNews.Hot = ConverterUlti.ConvertStringToLogic2(pHot);
                mNews.Slider = ConverterUlti.ConvertStringToLogic2(pSlide);
                mNews.Fast = ConverterUlti.ConvertStringToLogic2(pFast);
                mNews.Featured = ConverterUlti.ConvertStringToLogic2(pFeatured);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Lưu tin tức thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy tin tức để sửa." });

        }

    }
}