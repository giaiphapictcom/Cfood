using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Admin.Models.UI;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Nhóm sản phẩm")]
    public class ProductTypeController : BaseController
    {       
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index(int rootId =0, int parentId =0, int childId =0)
        {
            ViewBag.ListRoot = ProductTypeService.GetListByType((int)ProductCategoryLevelEnum.Root, 0);
            ViewBag.ListParent = ProductTypeService.GetListByType((int)ProductCategoryLevelEnum.Parent, rootId);
            ViewBag.ListChild = ProductTypeService.GetListByType((int)ProductCategoryLevelEnum.Child, parentId);
            var data = ProductTypeService.GetList(rootId, parentId, childId);
            var model = new ProductTypeViewModels
            {
                RootId = rootId,
                ParentId = parentId,
                ChildId = childId,
                Data = data
            };
            return View("Index", model);
        }      
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            ViewBag.ListCategory = BuildListCategory();
            return View("Create", new ProductTypeModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]        
        public ActionResult OnCreate(ProductTypeModels productType)
        {
            if (ModelState.IsValid)
            {
                productType.ImageUrl = productType.Image != null ?
                    productType.Image.Upload() :
                    productType.ImageUrl;
                productType.ImageBannerUrl = productType.ImageBanner != null ?
                    productType.ImageBanner.Upload() :
                    productType.ImageBannerUrl;
                var result = ProductTypeService.Insert(
                    productType.Name,
                    productType.ParentId,
                    productType.Icon,
                    productType.Description,
                    productType.ImageUrl,
                    productType.ImageBannerUrl,
                    productType.Number,
                    productType.CreatedDate,
                    productType.Status,
                    productType.IsHome
                    );
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Loại sản phẩm '{productType.Name}' đã tồn tại trên hệ thống.");
                    ViewBag.ListCategory = BuildListCategory();
                    return View("Create", productType);

                }
                SetFlashMessage($"Thêm loại sản phẩm '{productType.Name}' thành công.");
                if (productType.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListCategory = BuildListCategory();
                ModelState.Clear();
                return View("Create", productType.ResetValue());
            }
            ViewBag.ListCategory = BuildListCategory();
            return View("Create", productType);

        }      
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var productTypeItem = ProductTypeService.Find(id);
            if (productTypeItem == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ListCategory = BuildListCategory();
            var productTypeModel = new ProductTypeModels
            {
                Id = productTypeItem.ID,
                Name = productTypeItem.Name,
                ParentId = productTypeItem.Parent ?? 0,
                Number = productTypeItem.Number ?? 0,
                Status = productTypeItem.Status.HasValue && productTypeItem.Status.Value,
                ImageUrl = productTypeItem.Image,
                ImageBannerUrl = productTypeItem.ImageBanner,
                Icon = productTypeItem.Icon,
                Description = productTypeItem.Description

            };
            return View("Edit", productTypeModel);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnEdit(ProductTypeModels productType)
        {
            if (ModelState.IsValid)
            {
                productType.ImageUrl = productType.Image != null ?
                    productType.Image.Upload() :
                    productType.ImageUrl.ToImageOriginalPath();

                productType.ImageBannerUrl = productType.ImageBanner != null ?
                  productType.ImageBanner.Upload() :
                  productType.ImageBannerUrl.ToImageOriginalPath();

                var result = ProductTypeService.Update(
                    productType.Id,
                    productType.Name,
                    productType.ParentId,
                    productType.Icon,
                    productType.Description,
                    productType.ImageUrl,
                    productType.ImageBannerUrl,
                    productType.Number,
                    productType.CreatedDate,
                    productType.Status,
                    productType.IsHome
                    );
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Loại sản phẩm không tồn tại trên hệ thống.");
                    ViewBag.ListCategory = BuildListCategory();
                    return View("Edit", productType);
                }
                SetFlashMessage($"Cập nhật loại sản phẩm '{productType.Name}' thành công.");
                if (productType.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListCategory = BuildListCategory();
                return View("Edit", productType);

            }
            ViewBag.ListCategory = BuildListCategory();
            return View("Edit", productType);
        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = ProductTypeService.Delete(id);
            SetFlashMessage(result == Result.Ok ? "Xóa Loại sản phẩm thành công." :
                "Loại sản phẩm không tồn tại trên hệ thống.");
            return RedirectToAction("Index");

        }
        [HttpPost]
        [CheckPermission(4, "Thay đổi trạng thái")]        
        [ActionName("ChangeState")]
        public ActionResult OnChangeState(int id)
        {
            var result = ProductTypeService.ChangeState(id);
            SetFlashMessage(result == Result.Ok ?
                "Thay đổi trạng thái Loại sản phẩm thành công." :
                "Loại sản phẩm không tồn tại trên hệ thống.");
            return RedirectToAction("Index");

        }
        [NonAction]
        private List<MutilCategoryItem> BuildListCategory()
        {
            return ProductTypeService.GetAll().Select
                (
                    cate => new MutilCategoryItem
                    {
                        Name = cate.Name,
                        Id = cate.ID,
                        ParentId = cate.Parent
                    }
                ).ToList();
        }
       
        
    }
}