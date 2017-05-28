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
       
        private const int  PageSize =30;
        [CheckAdminAuthorize(2)]
        public ActionResult Index(string keyword, int rootId = 0, int parentId = 0, int childId = 0, int pPage =1)
        {            
            NewsGroupPage mNewsGroupPage = new NewsGroupPage();           
            var mNewsGroupsList = NewsGroupService.GetList(keyword, rootId, parentId, childId,pPage, PageSize);
            if (mNewsGroupsList.Count < PageSize)
                mNewsGroupPage.IsEnd = true;
            //Tao Html cho danh sach tin nay
            mNewsGroupPage.Html = V308HTMLHELPER.TaoDanhSachCacNhomTinTuc(mNewsGroupsList, pPage);
            mNewsGroupPage.Page = (int)pPage;
            mNewsGroupPage.Keyword = keyword;
            mNewsGroupPage.RootId = rootId;
            mNewsGroupPage.ListNewsGroupRoot = NewsGroupService.GetListRoot();
            mNewsGroupPage.ParentId = parentId;
            mNewsGroupPage.ListNewsGroupParent = NewsGroupService.GetListParent(rootId);
            mNewsGroupPage.ChildId = childId;
            mNewsGroupPage.ListNewsGroupChild = NewsGroupService.GetListParent(parentId);
            return View("Index",mNewsGroupPage);
        }       
        [CheckAdminJson(2)]
        [HttpPost]      
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