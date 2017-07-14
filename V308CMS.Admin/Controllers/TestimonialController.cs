using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;
using V308CMS.Data;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Testimonial")]
    public class TestimonialController : BaseController
    {

        [CheckPermission(0, "Danh sách")]
        public ActionResult Index(string site = "")
        {
            return View("Index", TestimonialService.GetList(site));
        }

        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
           return View("Create", new TestimonialModels());
        }

        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult OnCreate(TestimonialModels comment)
        {
            if (ModelState.IsValid)
            {
                comment.Avartar = comment.Upload != null ?
                    comment.Upload.Upload() :
                    comment.Avartar;
                if (comment.Avartar != null)
                {
                    comment.Avartar = comment.Avartar.Replace("\\Content\\Images\\", "");
                }

                var newComment = comment.CloneTo<Testimonial>(new[] {
                   "Upload",

                });

                var result = TestimonialService.Insert(newComment);
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", string.Format("Banner '{0}' đã tồn tại trên hệ thống.", comment.Fullname));
                    ViewBag.ListSite = DataHelper.ListEnumTypeSepecial<SiteEnum>();
                    ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
                    return View("Create", comment);
                }
                SetFlashMessage(string.Format("Thêm banner '{0}' thành công.", comment.Fullname));
                if (comment.SaveList)
                {
                    string actionReturn = comment.Site == "affiliate" ? "affiliatebanner" : "Index";
                    return RedirectToAction(actionReturn);
                }
                ModelState.Clear();
                return View("Create", comment.ResetValue());
            }

            return View("Create", comment);
        }

        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var comment = TestimonialService.Find(id);
            if (comment == null)
            {
                return RedirectToAction("Index");

            }
            ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
            ViewBag.ListSite = DataHelper.ListEnumTypeSepecial<SiteEnum>();
            var CommentEdit = comment.CloneTo<TestimonialModels>(new[] {
                   "Upload"
                });

            
            return View("Edit", CommentEdit);
        }

        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult OnEdit(TestimonialModels comment)
        {
            if (ModelState.IsValid)
            {
                comment.Avartar = comment.Upload != null ?
                   comment.Upload.Upload() :
                   comment.Avartar.ToImageOriginalPath();
                var commentUpdate = comment.CloneTo<Testimonial>(new[] {"Upload" });

                if (commentUpdate.avartar != null)
                {
                    commentUpdate.avartar = commentUpdate.avartar.Replace("\\Content\\Images\\", "");
                }

                
                var result = TestimonialService.Update(commentUpdate);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Comment không tồn tại trên hệ thống.");
                    ViewBag.ListSite = DataHelper.ListEnumTypeSepecial<SiteEnum>();
                    ViewBag.ListPosition = DataHelper.ListEnumType<PositionEnum>();
                    return View("Edit", comment);
                }
                SetFlashMessage(string.Format("Cập nhật Banner '{0}' thành công.", comment.Fullname));
                if (comment.SaveList)
                {
                    string actionReturn = comment.Site == "affiliate" ? "affiliatebanner" : "Index";
                    return RedirectToAction(actionReturn);

                }
                
                return View("Edit", comment);
            }
           
            return View("Edit", comment);
        }

        [HttpPost]
        [CheckPermission(3, "Xóa")]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var comment = TestimonialService.Find(id);
            var result = TestimonialService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa banner thành công." :
                "Banner không tồn tại trên hệ thống.");

            string actionReturn = comment.site == "affiliate" ? "affiliateComment" : "Index";
            return RedirectToAction(actionReturn);
        }

        [HttpPost]
        [CheckPermission(4, "Thay đổi trạng thái")]
        [ActionName("ChangeStatus")]
        public ActionResult OnChangeStatus(int id)
        {
            var comment = TestimonialService.Find(id);
            var result = TestimonialService.ChangeStatus(id);
            SetFlashMessage(result == Result.Ok ?
                "Thay đổi trạng thái banner thành công." :
                "Banner không tồn tại trên hệ thống.");
            string actionReturn = comment.site == "affiliate" ? "affiliateComment" : "Index";
            return RedirectToAction(actionReturn);
        }

    }
}
