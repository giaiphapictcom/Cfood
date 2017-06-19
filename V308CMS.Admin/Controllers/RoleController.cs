using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Enum;
using V308CMS.Data.Models;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Nhóm tài khoản")]
    public class RoleController : BaseController
    {
        //
        // GET: /Rolo/       
        [NonAction]
        private void AddOrUpdateGroupPermission(int roleId, bool isAddOrUpdate = false, params object[] groupPermission)
        {
            for (int i = 0; i < groupPermission.Length; i+=2)
            {
                var groupName = groupPermission[i].ToString();
                var groupValue = (int[])groupPermission[i+1];
                AddPermission(roleId, groupName, groupValue, isAddOrUpdate);
            }
            
        }
        [NonAction]
        private void AddPermission(int roleId, string groupPermission,int[] listPermission,bool isAddOrUpdate = false)
        {
            var permission = new Permission
            {
                RoleId = roleId,
                GroupPermission = groupPermission,
                Value = listPermission?.Sum() ?? 0
            };
            if (isAddOrUpdate)
            {
                PermissionService.CreateOrUpdate(permission);
            }
            else
            {
                PermissionService.Insert(permission);
            }           
        }
        [NonAction]
        private int BindGroupValuePermission(ICollection<Permission> listPermission, string group)
        {
            
            var permissionItem = listPermission.
                FirstOrDefault(permission => permission.GroupPermission == group);
            return permissionItem?.Value ?? 0;
        }

        private void AddOrUpdateAllGroupPermission(RoleModels role, int roleId, bool isAddOrUpdate = false)
        {
            AddOrUpdateGroupPermission(roleId, isAddOrUpdate,
                    nameof(role.AdminAccountPermission), role.AdminAccountPermission,
                    nameof(role.RolePermission), role.RolePermission,
                    nameof(role.ContactPermission), role.ContactPermission,
                    nameof(role.SiteConfigPermission), role.SiteConfigPermission,
                    nameof(role.CountryPermission), role.CountryPermission,
                    nameof(role.EmailConfigPermission), role.EmailConfigPermission,
                    nameof(role.EmailPermission), role.EmailPermission,
                    nameof(role.MenuConfigPermission), role.MenuConfigPermission,
                    nameof(role.NewsCategoryPermission), role.NewsCategoryPermission,
                    nameof(role.NewsPermission), role.NewsPermission,
                    nameof(role.OrderPermission), role.OrderPermission,
                    nameof(role.ProductAttributePermission), role.ProductAttributePermission,
                    nameof(role.ProductBrandPermission), role.ProductBrandPermission,
                    nameof(role.ProductColorPermission), role.ProductColorPermission,
                    nameof(role.ProductImagePermission), role.ProductImagePermission,
                    nameof(role.ProductManufacturerPermission), role.ProductManufacturerPermission,
                    nameof(role.ProductUnitPermission), role.ProductUnitPermission,
                    nameof(role.SizePermission), role.SizePermission,
                    nameof(role.ProductDistributorPermission), role.ProductDistributorPermission,
                    nameof(role.ProductTypePermission), role.ProductTypePermission,
                    nameof(role.ProductPermission), role.ProductPermission,
                    nameof(role.VoucherPermission), role.VoucherPermission,
                    nameof(role.UserPermission), role.UserPermission,
                    nameof(role.BannerPermission), role.BannerPermission,
                    nameof(role.ProductStorePermission), role.ProductStorePermission

                    );
        }      
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            return View("Index", RoleService.GetList());
        }      
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            ViewBag.ListPermission = ControllerHelper.GetListControllerWithAction();
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();

            return View("Create", new RoleModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]        
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult OnCreate(RoleModels role)
        {
          
            if (ModelState.IsValid)
            {
               var roleId = RoleService.InsertAndReturnId(role.Name, role.Description,role.Status);
                if (roleId == 0)
                {
                    ModelState.AddModelError("", $"Nhóm tài khoản '{role.Name}' đã tồn tại trên hệ thống.");
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();                    
                    return View("Create", role);
                }
                AddOrUpdateAllGroupPermission(role, roleId);                                                                
                SetFlashMessage($"Thêm thông Nhóm tài khoản '{role.Name}' thành công.");
                if (role.SaveList)
                {
                    return RedirectToAction("Index");

                }             
                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                ViewBag.ListPermission = ControllerHelper.GetListControllerWithAction();
                ModelState.Clear();
                return View("Create", role.ResetValue());
            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            ViewBag.ListPermission = ControllerHelper.GetListControllerWithAction();
            return View("Create", role);
        }

        private void BindAllPermissionToRole(RoleModels model, ICollection<Permission> listPermission)
        {
            if (listPermission != null && listPermission.Count > 0)
            {
                model.AdminAccountPermissionAll = BindGroupValuePermission(listPermission, nameof(model.AdminAccountPermission));
                model.RolePermissionAll = BindGroupValuePermission(listPermission, nameof(model.RolePermission));
                model.ContactPermissionAll = BindGroupValuePermission(listPermission, nameof(model.ContactPermission));
                model.SiteConfigPermissionAll = BindGroupValuePermission(listPermission, nameof(model.SiteConfigPermission));
                model.CountryPermissionAll = BindGroupValuePermission(listPermission, nameof(model.CountryPermission));
                model.EmailConfigPermissionAll = BindGroupValuePermission(listPermission, nameof(model.EmailConfigPermission));
                model.EmailPermissionAll = BindGroupValuePermission(listPermission, nameof(model.EmailPermission));
                model.MenuConfigPermissionAll = BindGroupValuePermission(listPermission, nameof(model.MenuConfigPermission));
                model.NewsCategoryPermissionAll = BindGroupValuePermission(listPermission, nameof(model.NewsCategoryPermission));
                model.NewsPermissionAll = BindGroupValuePermission(listPermission, nameof(model.NewsPermission));
                model.OrderPermissionAll = BindGroupValuePermission(listPermission, nameof(model.OrderPermission));
                model.ProductAttributePermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductAttributePermission));
                model.ProductBrandPermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductBrandPermission));
                model.ProductColorPermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductColorPermission));
                model.ProductImagePermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductImagePermission));
                model.ProductManufacturerPermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductManufacturerPermission));
                model.ProductUnitPermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductUnitPermission));
                model.SizePermissionAll = BindGroupValuePermission(listPermission, nameof(model.SizePermission));
                model.ProductDistributorPermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductDistributorPermission));
                model.ProductTypePermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductTypePermission));
                model.ProductPermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductPermission));
                model.VoucherPermissionAll = BindGroupValuePermission(listPermission, nameof(model.VoucherPermission));
                model.UserPermissionAll = BindGroupValuePermission(listPermission, nameof(model.UserPermission));
                model.BannerPermissionAll = BindGroupValuePermission(listPermission, nameof(model.BannerPermission));
                model.ProductStorePermissionAll = BindGroupValuePermission(listPermission, nameof(model.ProductStorePermission));
            }

        }      
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var role = RoleService.FindToModify(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var model = new RoleModels
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                Status = role.Status               
            };
            BindAllPermissionToRole(model,role.Permissions);
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            ViewBag.ListPermission = ControllerHelper.GetListControllerWithAction();
            return View("Edit", model);

        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]        
        public ActionResult OnEdit(RoleModels role)
        {
            if (ModelState.IsValid)
            {
                AddOrUpdateAllGroupPermission(role, role.Id, true);
                var listPermission = RoleService.Update(role.Id, role.Name, role.Description, role.Status);
                if (listPermission == null)
                {
                    return RedirectToAction("Index");
                }                             
                 SetFlashMessage($"Cập nhật nhóm tài khoản '{role.Name}' thành công.");
                if (role.SaveList)
                {
                    return RedirectToAction("Index");
                }
                BindAllPermissionToRole(role, listPermission);
                ViewBag.ListPermission = ControllerHelper.GetListControllerWithAction();
                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                return View("Edit", role);
            }
            BindAllPermissionToRole(role, PermissionService.GetAllByRoleId(role.Id));
            ViewBag.ListPermission = ControllerHelper.GetListControllerWithAction();
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            return View("Edit", role);
        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]      
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = RoleService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa nhóm tài khoản thành công." :
                "Nhóm tài khoản không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }


    }
}

