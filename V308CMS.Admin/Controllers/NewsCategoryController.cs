using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Admin.Models.UI;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Nhóm tin")]
    public class NewsCategoryController : BaseController
    {
        [NonAction]
        private List<MutilCategoryItem> BuildListCategory()
        {
            return NewsGroupService.GetAll().Select
                (
                    cate => new MutilCategoryItem
                    {
                        Name = cate.Name,
                        Id = cate.ID,
                        ParentId = cate.Parent
                    }
                ).ToList();
        }        
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index(string site="home")
        {
            return View("Index", NewsGroupService.GetAll(false, site));
        }

        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create(string site = "home")
        {
            AddViewData("ListCategory", BuildListCategory());   
            var Model = new NewsCategoryModels();
            Model.Site = site;
            return View("Create", Model);
        }

        [HttpPost]
        [CheckPermission(1, "Thêm mới")]      
        [ActionName("Create")]
        public ActionResult OnCreate(NewsCategoryModels category)
        {
            if (ModelState.IsValid)
            {
                var result = NewsGroupService.Insert
                    (
                        category.Site,
                       category.Name,
                       category.Alias,
                       category.ParentId,
                       category.Number,
                       category.State,
                       category.CreatedDate
                    );
                if (result == "exists")
                {
                    ModelState.AddModelError("", string.Format("Chuyên mục '{0}' đã tồn tại trên hệ thống.",category.Name) );
                    AddViewData("ListCategory", BuildListCategory());
                    return View("Create", category);

                }

                SetFlashMessage(string.Format("Thêm chuyên mục tin '{0}' thành công.",category.Name) );
                string actionReturn = category.Site == "affiliate" ? "affiliatecategory" : "Index";
                return RedirectToAction(actionReturn);
            }

            AddViewData("ListCategory", BuildListCategory());
            return View("Create", category);
        }

        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var categoryItem = NewsGroupService.Find(id);
            if (categoryItem == null)
            {
                return RedirectToAction("Index");
            }
            AddViewData("ListCategory", BuildListCategory());
            var categoryModel = new NewsCategoryModels
            {
                Id = categoryItem.ID,
                Name = categoryItem.Name,
                Alias = categoryItem.Alias,
                ParentId = categoryItem.Parent ?? 0,
                Number = categoryItem.Number ?? 0,
                State = categoryItem.Status.HasValue && categoryItem.Status.Value,
                Site = categoryItem.Site
            };
            return View("Edit", categoryModel);
        }

        [HttpPost]
        [CheckPermission(2, "Sửa")]       
        [ActionName("Edit")]       
        [ValidateAntiForgeryToken]      
        public ActionResult OnEdit(NewsCategoryModels category)
        {
            if (ModelState.IsValid)
            {
                var result = NewsGroupService.Update(
                    category.Id,category.Name,category.Alias,
                    category.ParentId,category.Number,
                    category.State,category.CreatedDate);
                if (result == "not_exists")
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    AddViewData("ListCategory", BuildListCategory());
                    return View("Edit", category);
                }
                SetFlashMessage( string.Format("Sửa chuyên mục '{0}' thành công.",category.Name) );

                string actionReturn = category.Site == "affiliate" ? "affiliatecategory" : "Index";
                return RedirectToAction(actionReturn);

            }
            AddViewData("ListCategory", BuildListCategory());
            return View("Edit", category);

        }

        [HttpPost]
        [CheckPermission(3, "Thay đổi trạng thái")]
        [ActionName("ChangeState")]
        public ActionResult OnChangeState(int id)
        {
            var result = NewsGroupService.ChangeState(id);
            SetFlashMessage(result == "ok" ? 
                "Thay đổi trạng thái Chuyên mục thành công." :
                "Chuyên mục không tồn tại trên hệ thống.");
            return RedirectToAction("Index");

        }


        [HttpPost]
        [CheckPermission(4, "Xóa")]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var category = NewsGroupService.Find(id);
            var result = NewsGroupService.Delete(id);
            SetFlashMessage(result == "ok" ? 
                "Xóa chuyên mục thành công." :
                "Chuyên mục không tồn tại trên hệ thống.");
            string actionReturn = category.Site == "affiliate" ? "affiliatecategory" : "Index";
            return RedirectToAction(actionReturn);
        }


        #region affiliate
        [CheckPermission(1, "Thêm mới")]
        public ActionResult affiliatecategory()
        {
            return Index("affiliate");
        }

        [CheckPermission(1, "Thêm mới")]
        public ActionResult affiliateCreate() {
            return Create("affiliate");
        }
        #endregion

    }
}