using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IPermissionRespository
    {
        List<Permission> GetAllByRoleId(int roleId);
        string DeleteAllByRoleId(int roleId);
        string CreateOrUpdate(Permission data);
        int GetPermissionValueByGroupAndRole(string groupPermission, int roleId);
    }
    public class PermissionRespository: IBaseRespository<Permission>, IPermissionRespository
    {
        public PermissionRespository()
        {
           
           
        }
        public Permission Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from permission in entities.Permission
                        where permission.Id == id
                        select permission
               ).FirstOrDefault();
            }
           
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var permissionDelete = (from permission in entities.Permission
                                        where permission.Id == id
                                        select permission
                   ).FirstOrDefault();
                if (permissionDelete != null)
                {
                    entities.Permission.Remove(permissionDelete);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }
            
        }

        public string Update(Permission data)
        {
            using (var entities = new V308CMSEntities())
            {
                var permissionUpdate = (from permission in entities.Permission
                                        where permission.Id == data.Id
                                        select permission
              ).FirstOrDefault();
                if (permissionUpdate != null)
                {
                    permissionUpdate.GroupPermission = data.GroupPermission;
                    permissionUpdate.Value = data.Value;
                    permissionUpdate.RoleId = data.RoleId;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Insert(Permission data)
        {
            using (var entities = new V308CMSEntities())
            {
                var permissionInsert = (from permission in entities.Permission
                                        where permission.GroupPermission == data.GroupPermission                                       
                                        select permission
               ).FirstOrDefault();
                if (permissionInsert == null){
                    entities.Permission.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";

            }
        }

        public List<Permission> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from permission in entities.Permission
                        orderby permission.Id descending
                        select permission
              ).ToList();
            }
          
        }

        public List<Permission> GetAllByRoleId(int roleId)
        {
            List<Permission> roles = new List<Permission>();
            using (var entities = new V308CMSEntities())
            {
                var role = from permission in entities.Permission
                        where permission.RoleId == roleId
                        orderby permission.Id descending
                        select permission;
                if (role.Count() > 0)
                {
                    roles =  role.ToList();
                }
            }
            return roles;

        }

        public string DeleteAllByRoleId(int roleId)
        {
            using (var entities = new V308CMSEntities())
            {
                var listPermission = (from permission in entities.Permission
                        where permission.RoleId == roleId
                        orderby permission.Id descending
                        select permission
              ).ToList();
                if (listPermission.Count > 0)
                {
                    foreach (var permission in listPermission)
                    {
                        entities.Permission.Remove(permission);
                        entities.SaveChanges();
                    }
                    return "ok";
                }
                return "not_exists";
            }
        }

        public string CreateOrUpdate(Permission data)
        {
            using (var entities = new V308CMSEntities())
            {
                var permissionUpdate = (from permission in entities.Permission
                                        where permission.GroupPermission == data.GroupPermission
                                        && permission.RoleId == data.RoleId
                                        select permission
                ).FirstOrDefault();
                if (permissionUpdate != null)
                {                    
                    if (permissionUpdate.Value != data.Value)
                    {
                        permissionUpdate.Value = data.Value;
                        entities.SaveChanges();
                        return "update_ok";
                    }                  
                    return "update_exists";
                }
                else
                {
                  string insertResult = Insert(data);
                  return string.Format("create_{0}", insertResult);
                }               
            }
        }

        public int GetPermissionValueByGroupAndRole(string groupPermission, int roleId)
        {
            using (var entities = new V308CMSEntities())
            {
                var permissionItem = (from permission in entities.Permission
                                      where permission.GroupPermission == groupPermission
                                      && permission.RoleId == roleId
                                      select permission
                ).FirstOrDefault();
                return permissionItem!= null ? permissionItem.Value : 0;
            }
            
        }
    }
}

