using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Data;
using V308CMS.Data.Helpers;


namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Affiliate Category")]
    public class AffiliateCategoryController : BaseController
    {
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            return View("Index", AffiliateCategoryRepo.GetList(1, ListViewLimit));
        }

        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            var Model = new AffiliateCategoryModels();
            return View("Create", Model);
        }

        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        public ActionResult OnCreate(AffiliateCategoryModels category)
        {
            if (ModelState.IsValid)
            {
                var cate = new Categorys {
                    id = category.id,
                    name = category.Name,
                    link = category.Link,
                    summary = category.Summary,
                    image = category.Image,
                    order = category.Order,
                    status = category.Status
                };

                cate.image = category.ImgNew != null ?
                    category.ImgNew.Upload() :
                    category.Image;

                var result = AffiliateCategoryRepo.Insert(cate);
                if (result == Data.Helpers.Result.Exists)
                {
                    ModelState.AddModelError("", string.Format("Chuyên mục '{0}' đã tồn tại trên hệ thống.", category.Name));
                    return View("Create", category);

                }
                else if (result == Data.Helpers.Result.Ok) {
                    SetFlashMessage(string.Format("Thêm chuyên '{0}' thành công.", category.Name));
                    return RedirectToAction("Index");
                }
                
            }

            
            return View("Create", category);
        }

        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var categoryItem = AffiliateCategoryRepo.Find(id);
            if (categoryItem == null)
            {
                return RedirectToAction("Index");
            }

            var categoryModel = new AffiliateCategoryModels
            {
                id = categoryItem.id,
                Name = categoryItem.name,
                Link = categoryItem.link,
                Summary = categoryItem.summary,
                Image = categoryItem.image,
                Order = categoryItem.order,
                Status = (categoryItem.status)
               
            };
            return View("Edit", categoryModel);
        }

        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult OnEdit(AffiliateCategoryModels category)
        {
            if (ModelState.IsValid)
            {
                var cate = new Categorys
                {
                    id = category.id,
                    name = category.Name,
                    link = category.Link,
                    summary = category.Summary,
                    image = category.Image,
                    order = category.Order,
                    status = category.Status
                };
                cate.image = category.ImgNew != null ?
                    category.ImgNew.Upload() :
                    category.Image;

                var result = AffiliateCategoryRepo.Update(cate);
                if (result == Data.Helpers.Result.NotExists)
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    return View("Edit", category);
                }
                else if (result == Data.Helpers.Result.Ok) {
                    SetFlashMessage(string.Format("Sửa chuyên mục '{0}' thành công.", category.Name));
                    return RedirectToAction("Index");
                }
            }
            return View("Edit", category);

        }


        [HttpPost]
        [CheckPermission(3, "Thay đổi trạng thái")]
        [ActionName("ChangeState")]
        public ActionResult OnChangeState(int id)
        {
            var result = AffiliateCategoryRepo.ChangeState(id);
            SetFlashMessage(result == Data.Helpers.Result.Ok ? "Thay đổi trạng thái Chuyên mục thành công." : "Chuyên mục không tồn tại trên hệ thống.");
            return RedirectToAction("Index");

        }


        [HttpPost]
        [CheckPermission(4, "Xóa")]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = AffiliateCategoryRepo.Delete(id);
            SetFlashMessage(result == Data.Helpers.Result.Ok ? "Xóa chuyên mục thành công." : "Chuyên mục không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }
    }



}
