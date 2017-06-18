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
        public ActionResult Index()
        {            
            return View("Index",NewsGroupService.GetAll(false));
        }        
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            AddViewData("ListCategory", BuildListCategory());         
            return View("Create", new NewsCategoryModels());
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
                       category.Name,
                       category.ParentId,
                       category.Number,
                       category.State,
                       category.CreatedDate
                    );
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Chuyên mục '{category.Name}' đã tồn tại trên hệ thống.");
                    AddViewData("ListCategory", BuildListCategory());
                    return View("Create", category);

                }

                SetFlashMessage($"Thêm chuyên mục tin '{category.Name}' thành công.");
                return RedirectToAction("Index");
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
                ParentId = categoryItem.Parent ?? 0,
                Number = categoryItem.Number ?? 0,
                State = categoryItem.Status.HasValue && categoryItem.Status.Value
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
                    category.Id,category.Name,
                    category.ParentId,category.Number,
                    category.State,category.CreatedDate);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    AddViewData("ListCategory", BuildListCategory());
                    return View("Edit", category);
                }
                SetFlashMessage($"Sửa chuyên mục '{category.Name}' thành công.");
                return RedirectToAction("Index");
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
            SetFlashMessage(result == Result.Ok ? 
                "Thay đổi trạng thái Chuyên mục thành công." :
                "Chuyên mục không tồn tại trên hệ thống.");
            return RedirectToAction("Index");

        }       
        [HttpPost]
        [CheckPermission(4, "Xóa")]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = NewsGroupService.Delete(id);
            SetFlashMessage(result == Result.Ok ? 
                "Xóa chuyên mục thành công." :
                "Chuyên mục không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }

       

    }
}