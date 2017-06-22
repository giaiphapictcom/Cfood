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
                Value = listPermission != null && listPermission.Length>0? listPermission.Sum() :0
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
            int value = 0;
            var permissionItem = listPermission.
                FirstOrDefault(permission => permission.GroupPermission == group);          
            if (permissionItem != null) {
                value = permissionItem.Value;
            }

            return value;
        }

        private void AddOrUpdateAllGroupPermission(RoleModels role, int roleId, bool isAddOrUpdate = false)
        {
            AddOrUpdateGroupPermission(roleId, isAddOrUpdate,
                    "AdminAccountPermission", role.AdminAccountPermission,
                    "RolePermission", role.RolePermission,
                    "ContactPermission", role.ContactPermission,
                    "SiteConfigPermission", role.SiteConfigPermission,
                    "CountryPermission", role.CountryPermission,
                    "EmailConfigPermission", role.EmailConfigPermission,
                    "EmailPermission", role.EmailPermission,
                    "MenuConfigPermission", role.MenuConfigPermission,
                    "NewsCategoryPermission", role.NewsCategoryPermission,
                    "NewsPermission", role.NewsPermission,
                    "OrderPermission", role.OrderPermission,
                    "ProductAttributePermission", role.ProductAttributePermission,
                    "ProductBrandPermission", role.ProductBrandPermission,
                    "ProductColorPermission", role.ProductColorPermission,
                    "ProductImagePermission", role.ProductImagePermission,
                    "ProductManufacturerPermission", role.ProductManufacturerPermission,
                    "ProductUnitPermission", role.ProductUnitPermission,
                    "SizePermission", role.SizePermission,
                    "ProductDistributorPermission", role.ProductDistributorPermission,
                    "ProductTypePermission", role.ProductTypePermission,
                    "ProductPermission", role.ProductPermission,
                    "VoucherPermission", role.VoucherPermission,
                    "UserPermission", role.UserPermission,
                    "BannerPermission", role.BannerPermission,
                    "ProductStorePermission", role.ProductStorePermission,
                    "ProfilePermission", role.ProfilePermission
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
                    ModelState.AddModelError("", string.Format("Nhóm tài khoản '{0}' đã tồn tại trên hệ thống.",role.Name) );
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();                    
                    return View("Create", role);
                }
                AddOrUpdateAllGroupPermission(role, roleId);                                                                
                SetFlashMessage( string.Format("Thêm thông Nhóm tài khoản '{0}' thành công.",role.Name) );
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

                model.ProfilePermissionAll = BindGroupValuePermission(listPermission, "ProfilePermission");
                model.AdminAccountPermissionAll = BindGroupValuePermission(listPermission, "AdminAccountPermission");
                model.RolePermissionAll = BindGroupValuePermission(listPermission, "RolePermission");
                model.ContactPermissionAll = BindGroupValuePermission(listPermission, "ContactPermission");
                model.SiteConfigPermissionAll = BindGroupValuePermission(listPermission, "SiteConfigPermission");
                model.CountryPermissionAll = BindGroupValuePermission(listPermission, "CountryPermission");
                model.EmailConfigPermissionAll = BindGroupValuePermission(listPermission, "EmailConfigPermission");
                model.EmailPermissionAll = BindGroupValuePermission(listPermission, "EmailPermission");
                model.MenuConfigPermissionAll = BindGroupValuePermission(listPermission, "MenuConfigPermission");
                model.NewsCategoryPermissionAll = BindGroupValuePermission(listPermission, "NewsCategoryPermission");
                model.NewsPermissionAll = BindGroupValuePermission(listPermission, "NewsPermission");
                model.OrderPermissionAll = BindGroupValuePermission(listPermission, "OrderPermission");
                model.ProductAttributePermissionAll = BindGroupValuePermission(listPermission, "ProductAttributePermission");
                model.ProductBrandPermissionAll = BindGroupValuePermission(listPermission, "ProductBrandPermission");
                model.ProductColorPermissionAll = BindGroupValuePermission(listPermission, "ProductColorPermission");
                model.ProductImagePermissionAll = BindGroupValuePermission(listPermission, "ProductImagePermission");
                model.ProductManufacturerPermissionAll = BindGroupValuePermission(listPermission, "ProductManufacturerPermission");
                model.ProductUnitPermissionAll = BindGroupValuePermission(listPermission, "ProductUnitPermission");
                model.SizePermissionAll = BindGroupValuePermission(listPermission, "SizePermission");
                model.ProductDistributorPermissionAll = BindGroupValuePermission(listPermission, "ProductDistributorPermission");
                model.ProductTypePermissionAll = BindGroupValuePermission(listPermission, "ProductTypePermission");
                model.ProductPermissionAll = BindGroupValuePermission(listPermission, "ProductPermission");
                model.VoucherPermissionAll = BindGroupValuePermission(listPermission, "VoucherPermission");
                model.UserPermissionAll = BindGroupValuePermission(listPermission, "UserPermission");
                model.BannerPermissionAll = BindGroupValuePermission(listPermission, "BannerPermission");
                model.ProductStorePermissionAll = BindGroupValuePermission(listPermission, "ProductStorePermission");
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
                SetFlashMessage(string.Format("Cập nhật nhóm tài khoản '{0}' thành công.", role.Name));
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

