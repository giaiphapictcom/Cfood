using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IPermissionRespository
    {
        
    }
    public class PermissionRespository: IBaseRespository<Permission>, IPermissionRespository
    {
        private readonly V308CMSEntities _entities;

        public PermissionRespository(V308CMSEntities entities)
        {
            _entities = entities;
        }
      
        public Permission Find(int id)
        {
            return (from permission in _entities.Permission
                where permission.Id == id
                select permission
                ).FirstOrDefault();
        }

        public string Delete(int id)
        {
            var permissionDelete = (from permission in _entities.Permission
                              where permission.Id == id
                              select permission
                ).FirstOrDefault();
            if (permissionDelete != null)
            {
                _entities.Permission.Remove(permissionDelete);
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Update(Permission data)
        {
            var permissionUpdate = (from permission in _entities.Permission
                                    where permission.Id == data.Id
                                    select permission
               ).FirstOrDefault();
            if (permissionUpdate != null)
            {
                permissionUpdate.GroupId = data.GroupId;
                permissionUpdate.Action = data.Action;
                permissionUpdate.Value = data.Value;
                permissionUpdate.Status = data.Status;
                permissionUpdate.CreatedAt = data.CreatedAt;
                permissionUpdate.UpdatedAt = data.UpdatedAt;
                _entities.SaveChanges();
                return "ok";
            }
            return "not_exists";
        }

        public string Insert(Permission data)
        {
            var permissionInsert= (from permission in _entities.Permission
                                    where permission.GroupId == data.GroupId
                                    orderby permission.Value descending 
                                    select permission
                ).FirstOrDefault();
            if (permissionInsert == null)
            {
                data.Value = 1;
            }
            else
            {
                data.Value = permissionInsert.Value*2;
            }
            _entities.Permission.Add(data);
            _entities.SaveChanges();
            return "ok";
        }

        public List<Permission> GetAll()
        {
            return (from permission in _entities.Permission
                orderby permission.UpdatedAt descending
                select permission
                ).ToList();
        }
    }
}

