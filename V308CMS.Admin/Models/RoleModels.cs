using System.ComponentModel.DataAnnotations;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Models
{
    public class BaseRoleModels : BaseModels
    {
        public BaseRoleModels()
        {
            Status = (byte)StateEnum.Active;
        }
        public int Id { get; set; }

        [Display(Name = "Tên nhóm quyền*")]
        [Required(ErrorMessage = "Tên nhóm quyền không được để trống")]
        [StringLength(250, ErrorMessage = "Tên nhóm quyền không được vượt quá 250 ký tự.")]
        public string Name { get; set; }
        [Display(Name = "Mô tả nhóm quyền")]
        [StringLength(500, ErrorMessage = "Mô tả nhóm quyền không được vượt quá 500 ký tự.")]
        public string Description { get; set; }
        [Display(Name = "Trạng thái")]
        public byte Status { get; set; }
    }
    public class RoleModels: BaseRoleModels
    {
        public int AllPermission { get; set; }
        public int AdminAccountPermissionAll { get; set; }
        public int ContactPermissionAll { get; set; }
        public int CountryPermissionAll { get; set; }
        public int EmailConfigPermissionAll { get; set; }
        public int EmailPermissionAll { get; set; }
        public int MenuConfigPermissionAll { get; set; }
        public int NewsCategoryPermissionAll { get; set; }
        public int NewsPermissionAll { get; set; }
        public int OrderPermissionAll { get; set; }
        public int ProductAttributePermissionAll { get; set; }
        public int ProductBrandPermissionAll { get; set; }
        public int ProductColorPermissionAll { get; set; }
        public int ProductPermissionAll { get; set; }
        public int ProductDistributorPermissionAll { get; set; }
        public int ProductStorePermissionAll { get; set; }
        public int ProductImagePermissionAll { get; set; }
        public int ProductManufacturerPermissionAll { get; set; }
        public int ProductTypePermissionAll { get; set; }
        public int ProductUnitPermissionAll { get; set; }
        public int RolePermissionAll { get; set; }
        public int SiteConfigPermissionAll { get; set; }
        public int SizePermissionAll { get; set; }
        public int UserPermissionAll { get; set; }
        public int VoucherPermissionAll { get; set; }
        public int BannerPermissionAll { get; set; }

      


        public int[] BannerPermission { get; set; }       
        public int[] AdminAccountPermission { get; set; }       
        public int[] ContactPermission { get; set; }        
        public int[] CountryPermission { get; set; }       
        public int[] EmailConfigPermission { get; set; }      
        public int[] EmailPermission { get; set; }        
        public int[] MenuConfigPermission { get; set; }     
        public int[] NewsCategoryPermission { get; set; }       
        public int[] NewsPermission { get; set; }     
        public int[] OrderPermission { get; set; }        
        public int[] ProductAttributePermission { get; set; }       
        public int[] ProductBrandPermission { get; set; }
        public int[] ProductColorPermission { get; set; }
        public int[] ProductPermission { get; set; }
        public int[] ProductDistributorPermission { get; set; }
        public int[] ProductImagePermission { get; set; }
        public int[] ProductManufacturerPermission { get; set; }        
        public int[] ProductStorePermission { get; set; }
        public int[] ProductTypePermission { get; set; }
        public int[] ProductUnitPermission { get; set; }      
        public int[] RolePermission { get; set; }        
        public int[] SiteConfigPermission { get; set; }       
        public int[] SizePermission { get; set; }        
        public int[] UserPermission { get; set; }       
        public int[] VoucherPermission { get; set; }     
    }
}