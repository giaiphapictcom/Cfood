using System;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [CustomAuthorize]
    public class NewsCategoryController : BaseController
    {
        #region THE LOAI TIN TUC

        [CheckAdminAuthorize(2)]
        public ActionResult Index(int? pType, int? pPage)
        {            
            NewsGroupPage mNewsGroupPage = new NewsGroupPage();
            string mLevel = "";           
            if (pType == null)
            {
                if (Session["LoaiTinTucType"] != null)
                    pType = (int)Session["LoaiTinTucType"];
                else
                    pType = 0;
            }
            else
            {
                Session["LoaiTinTucType"] = pType;
            }
            if (pPage == null)
            {
                if (Session["LoaiTinTucPage"] != null)
                    pPage = (int)Session["LoaiTinTucPage"];
                else
                    pPage = 1;
            }
            else
            {
                Session["LoaiTinTucPage"] = pPage;
            }
            #endregion
            //lay Level cua Type
            if (pType != 0)
            {
                var mNewsGroups = NewsService.LayTheLoaiTinTheoId((int)pType);
                if (mNewsGroups != null)
                    mLevel = mNewsGroups.Level.Trim();
            }
            /*Lay danh sach cac tin theo page*/
            var mNewsGroupsListAll = NewsService.LayNhomTinAll();
            var mNewsGroupsList = NewsService.LayNewsGroupsTrangAndGroupIdAdmin((int)pPage, 10, (int)pType, mLevel);
            if (mNewsGroupsList.Count < 10)
                mNewsGroupPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mNewsGroupPage.Html = V308HTMLHELPER.TaoDanhSachCacNhomTinTuc(mNewsGroupsList, (int)pPage);
            mNewsGroupPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTinHome2(mNewsGroupsListAll, (int)pPage, (int)pType);
            mNewsGroupPage.Page = (int)pPage;
            mNewsGroupPage.TypeId = (int)pType;
            return View(mNewsGroupPage);
        }       
        [CheckAdminJson(2)]
        [HttpPost]
        [ActionName("Delete")]
        public JsonResult OnDelete(int pId = 0)
        {
           
           
            var mNewsGroups = NewsService.LayTheLoaiTinTheoId(pId);
            if (mNewsGroups != null)
            {
                MpStartEntities.DeleteObject(mNewsGroups);
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Xóa thành công!" });
            }
            return Json(new { code = 0, message = "Không tìm thấy tin cần xóa." });

        }       
        [CheckAdminJson(2)]
        [HttpPost]
        [ActionName("ChangeStatus")]
        public JsonResult OnChangeStatus(int pId = 0)
        {                    
            var mNewsGroups = NewsService.LayTheLoaiTinTheoId(pId);
            if (mNewsGroups != null)
            {
                mNewsGroups.Status = !mNewsGroups.Status;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message =(mNewsGroups.Status == true? "Hiện thể loại tin thành công!": "Ẩn thể loại tin thành công!")  });
            }
            return Json(new { code = 0, message = "Không tìm thấy thể loại tin cần ẩn." });


        }               
        
        [CheckAdminAuthorize(2)]
        public ActionResult Create()
        {           
            NewsGroupPage mNewsGroupPage = new NewsGroupPage();
            var mListNewsGroup = NewsService.LayNhomTinAll();
            //Tao danh sach cac nhom tin
            mNewsGroupPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTin2(mListNewsGroup, 0);
            return View("Create", mNewsGroupPage);
        }
        [HttpPost]      
        [CheckAdminJson(2)]
        [ValidateInput(false)]
        [ActionName("Create")]
        public JsonResult OnCreate(string pTieuDe, string pLink, int? pKichHoat, int? pUuTien, int? pGroupId = 0)
        {          
            NewsGroups mNewsGroups;
            string[] mLevelArray;
            long mLevel = 0;
            if (pGroupId == 0)
            {
                //Tinh gia tri Level moi cho Group nay
                //1- Lay tat ca cac Group me
                //2- Convert gia tri Level de lay gia tri lon nhat
                //3- Tao gia tri moi lon hon gia tri lon nhat
                mLevelArray = (from p in MpStartEntities.NewsGroups
                               where p.Parent == 0
                               select p.Level).ToArray();
                mLevel = mLevelArray.Select(p => Convert.ToInt64(p)).ToArray().Max();
                mLevel = (mLevel + 1);
                mNewsGroups = new NewsGroups() { Link = pLink, Date = DateTime.Now, Number = pUuTien, Status = true, Name = pTieuDe, Parent = pGroupId, Level = mLevel.ToString() };
                MpStartEntities.AddToNewsGroups(mNewsGroups);
                MpStartEntities.SaveChanges();
            }
            else
            {
                //lay level cua nhom me
                NewsGroups mNewsGroupsParent = NewsService.LayTheLoaiTinTheoId((int)pGroupId);
                if (mNewsGroupsParent != null)
                {
                    mLevelArray = (from p in MpStartEntities.NewsGroups
                                   where (p.Level.Substring(0, mNewsGroupsParent.Level.Length).Equals(mNewsGroupsParent.Level)) && (p.Level.Length == (mNewsGroupsParent.Level.Length + 5))
                                   select p.Level).ToArray();
                    if (mLevelArray.Any())
                    {
                        mLevel = mLevelArray.Select(p => Convert.ToInt64(p)).ToArray().Max();
                        mLevel = (mLevel + 1);
                    }
                    else
                    {
                        mLevel = Convert.ToInt64(mNewsGroupsParent.Level.ToString().Trim() + "10001");
                    }
                    mNewsGroups = new NewsGroups() { Date = DateTime.Now, Number = pUuTien, Status = true, Name = pTieuDe, Parent = pGroupId, Level = mLevel.ToString() };
                    MpStartEntities.AddToNewsGroups(mNewsGroups);
                    MpStartEntities.SaveChanges();
                }
                else
                {
                    return Json(new { code = 0, message = "Không tìm thấy nhóm tin." });
                }
            }
            return Json(new { code = 1, message = "Lưu tin tức thành công." });

        }
        [CustomAuthorize]
        [CheckAdminAuthorize(2)]
        [ActionName("Edit")]
        public ActionResult Edit(int pId = 0)
        {         
            NewsGroupPage mNewsGroupPage = new NewsGroupPage();
            var mNewsGroups = NewsService.LayTheLoaiTinTheoId(pId);
            if (mNewsGroups != null)
            {
                var mListNewsGroup = NewsService.LayNhomTinAll();
                //Tao danh sach cac nhom tin
                mNewsGroupPage.HtmlNhomTin = V308HTMLHELPER.TaoDanhSachNhomTin3(mListNewsGroup, (int)mNewsGroups.Parent);
                mNewsGroupPage.pNewsGroups = mNewsGroups;
            }
            else
            {
                mNewsGroupPage.Html = "Không tìm thấy tin tức cần sửa.";
            }
            return View("Edit", mNewsGroupPage);
        }
        [HttpPost]       
        [CheckAdminJson(2)]
        [ValidateInput(false)]
        [ActionName("Edit")]
        public JsonResult OnEdit(int pId, string pTieuDe, string pLink, int? pGroupId, int? pKichHoat, int? pUuTien)
        {           
            var mNewsGroups = NewsService.LayTheLoaiTinTheoId(pId);
            if (mNewsGroups != null)
            {
                mNewsGroups.Name = pTieuDe;
                mNewsGroups.Date = DateTime.Now;
                mNewsGroups.Number = pUuTien;
                mNewsGroups.Parent = pGroupId;
                mNewsGroups.Link = pLink;
                MpStartEntities.SaveChanges();
                return Json(new { code = 1, message = "Sủa thể loại tin thành công." });
            }
            return Json(new { code = 0, message = "Không tìm thấy loại tin tức để sửa." });

        }
        
    }
}